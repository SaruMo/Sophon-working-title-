using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject globalSettings;
    public GameObject player;

    public Vector3 CameraPosition3D = new Vector3(1.23f, 2.4f, 10.1f);
    public Vector3 CameraAngle3D = new Vector3(22.489f, 75.417f, -5.527f);

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if (globalSettings.GetComponent<GlobalSettings>().Is2D)
        //{
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        var xAngle = -transform.rotation.x;
        var yAngle = -transform.rotation.y;
        var zAngle = -transform.rotation.z;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        //}
        //else
        //{
        //    transform.position = new Vector3(player.transform.position.x - 0.9f, player.transform.position.y + 0.6f, player.transform.position.z);
        //    transform.rotation = Quaternion.Euler(new Vector3(22.489f, 75.417f, -5.527f));
        //}
    }
}
