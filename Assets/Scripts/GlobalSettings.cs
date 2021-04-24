using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    private bool is2D = true;
    public GameObject Player;

    private List<Vector2> savePoints;
    private Vector2 m_latestSavePoint;
    private float m_YPositionBottomBoundary = -5.0f;

    // Start is called before the first frame update
    void Start()
    {
        savePoints = new List<Vector2>();
        AddSavePoint(Player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerInvalid())
        {
            ResetToLastSafePosition();
        }
    }

    bool IsPlayerInvalid()
    {
        return IsOutOfBounds();
    }

    bool IsOutOfBounds()
    {
        return Player.transform.position.y < m_YPositionBottomBoundary;
    }

    void ResetToLastSafePosition()
    {
        if (m_latestSavePoint != null)
        {
            Logging.LogComment(name, "Player restored to last save point");
            Player.transform.position = m_latestSavePoint;
            return;
        }
        Logging.LogComment(name, "no save point for player to return to. FATAL ERROR");
    }

    void AddSavePoint(Vector2 in_latestSavePoint)
    {
        if (in_latestSavePoint == null)
        {
            Logging.LogComment(name, "save point given is null");
            return;
        }
        Logging.LogComment(name, "Save point added: " + in_latestSavePoint);
        savePoints.Add(in_latestSavePoint);
        m_latestSavePoint = in_latestSavePoint;
    }
}
