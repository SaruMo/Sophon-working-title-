using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EPlatformContact
{
    eGrounded,
    eNotGrounded
}

public class PlayerMovement : MonoBehaviour
{
    public float topSpeed = 10f;
    public float jumpForce = 30f;
    [SerializeField] private LayerMask platformsLayerMask;

    private float movementDirection = 0.0f;
    private const uint cMAX_MID_AIR_BOOSTS = 1;
    private uint m_numberOfMidAirBoosts = 0;

    private Rigidbody2D m_rigidBody2D;
    private BoxCollider2D m_boxCollider2D;
    bool m_isMoving = false;
    EPlatformContact m_groundedStatus = EPlatformContact.eGrounded;
    Vector2 m_lastSafePosition;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        m_rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        m_boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            HorizontalMovement_();
        }
        PerformJump_();
    }

    void HorizontalMovement_()
    {
        movementDirection = Input.GetAxis("Horizontal");
        if (m_rigidBody2D.velocity != Vector2.zero && !m_isMoving)
        {
            Logging.LogComment(name, "Player has started moving " + movementDirection);
            m_isMoving = true;
        }
        else if (m_rigidBody2D.velocity == Vector2.zero && m_isMoving)
        {
            Logging.LogComment(name, "Player has stopped moving");
            m_isMoving = false;
        }
        m_rigidBody2D.velocity = new Vector2(movementDirection * topSpeed, m_rigidBody2D.velocity.y);
    }

    #region Jump
    void PerformJump_()
    {
        var jump = Input.GetKeyDown(KeyCode.Mouse0);
        if (jump && IsGrounded())
        {
            m_rigidBody2D.velocity += (Vector2.up * jumpForce);
        }
        else if (!IsGrounded())
        {
            var midAirBoostJump = Input.GetKeyDown(KeyCode.Mouse0);
            //mid-air boost
            //freeze time for a specified amount of time to choose the direction of a boost
            if (midAirBoostJump && m_numberOfMidAirBoosts < cMAX_MID_AIR_BOOSTS)
            {
                HorizontalMovement_();
                PerformMidAirBoost_();
                m_numberOfMidAirBoosts++;
            }
        }
    }

    void PerformMidAirBoost_()
    {
        // for now just a double jump but need to improve to boost in a particular direction
        var mousePos = Input.mousePosition;
        var screenToWorldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.transform.position.z));
        var angleFromPosToMouse = AngleBetweenVector2(transform.position, screenToWorldMousePos);
        Logging.LogComment(name, "Mouse position for mid-air boost " + mousePos);
        Logging.LogComment(name, "screenToWorld for mid-air boost " + screenToWorldMousePos);

        var xThrust = m_rigidBody2D.velocity.y > 0 ? jumpForce - m_rigidBody2D.velocity.y : Math.Abs(m_rigidBody2D.velocity.y) + jumpForce;
        m_rigidBody2D.velocity += new Vector2(m_rigidBody2D.velocity.x, xThrust);
    }

    float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 vec1Rotated90 = new Vector2(-vec1.y, vec1.x);
        float sign = (Vector2.Dot(vec1Rotated90, vec2) < 0) ? -1.0f : 1.0f;
        var angle = Vector2.Angle(vec1, vec2) * sign;
        Logging.LogComment(name, "AngleBetweenVector2( " + vec1 + ", " + vec2 + " ) = " + angle);

        return angle;
    }

    bool IsGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(m_boxCollider2D.bounds.center, m_boxCollider2D.bounds.size, 0f, Vector2.down, 1f, platformsLayerMask);
        bool collided = rayCastHit.collider != null;
        if (collided && m_groundedStatus == EPlatformContact.eNotGrounded)
        {
            m_groundedStatus = EPlatformContact.eGrounded;
            m_numberOfMidAirBoosts = 0;
            Logging.LogComment(name, "Jump finished");
        }
        else if (!collided && m_groundedStatus == EPlatformContact.eGrounded)
        {
            m_groundedStatus = EPlatformContact.eNotGrounded;
            Logging.LogComment(name, "Jump started");
            m_lastSafePosition = transform.position;
        }
        return collided;
    }

    #endregion
}
