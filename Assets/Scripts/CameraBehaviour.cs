using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject globalSettings;
    public GameObject player;
    public float YCameraOffset = 0.0f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y - YCameraOffset, transform.position.z);
        var xAngle = -transform.rotation.x;
        var yAngle = -transform.rotation.y;
        var zAngle = -transform.rotation.z;
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
