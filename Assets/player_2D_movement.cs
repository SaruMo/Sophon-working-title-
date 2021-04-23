using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_2D_movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var XMove = Input.GetAxis("Horizontal");
        if (Math.Abs(XMove) > 0)
        {
            transform.position += new Vector3(XMove, 0, 0) * Time.deltaTime;
        }

        var jump = Input.GetKeyDown(KeyCode.Space);
        if (jump)
        {
            transform.position += Vector3.up * Time.deltaTime;
        }
    }
}
