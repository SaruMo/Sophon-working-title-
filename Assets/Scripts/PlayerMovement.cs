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
    [SerializeField] private LayerMask platformsLayerMask;

    private float movementDirection = 0.0f;

    private Rigidbody2D m_rigidBody2D;
    private BoxCollider2D m_boxCollider2D;
    bool jumping = false;
    bool moving = false;
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
            movementDirection = Input.GetAxis("Horizontal");
            if(movementDirection != 0 && !moving)
            {
                Logging.LogComment(name, "attempting to move: " + movementDirection);
                moving = true;
            }
            else
            {
                moving = false;
            }
            m_rigidBody2D.velocity = new Vector2(movementDirection * topSpeed, m_rigidBody2D.velocity.y);
        }

        CheckJump_();

        var saveMe = Input.GetKeyDown(KeyCode.S);
        if(saveMe)
        {
            SaveMe();
        }
    }

    #region Jump
    void CheckJump_()
    {
        var jump = Input.GetKeyDown(KeyCode.Space);
        if (jump && IsGrounded())
        {
            m_rigidBody2D.velocity = Vector2.up * 50;
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(m_boxCollider2D.bounds.center, m_boxCollider2D.bounds.size, 0f, Vector2.down, 1f, platformsLayerMask);
        bool collided =  rayCastHit.collider != null;
        if(collided && m_groundedStatus == EPlatformContact.eNotGrounded)
        {
            m_groundedStatus = EPlatformContact.eGrounded;
            Logging.LogComment(name, "Jump finished");
        }
        else if(!collided && m_groundedStatus == EPlatformContact.eGrounded)
        {
            m_groundedStatus = EPlatformContact.eNotGrounded;
            Logging.LogComment(name, "Jump started");
            m_lastSafePosition = transform.position;
        }
        return collided;
    }

    #endregion

    void SaveMe()
    {
        Logging.LogComment(name, "Save me");
        m_rigidBody2D.velocity = Vector2.zero;
        transform.position = m_lastSafePosition;
    }
}
