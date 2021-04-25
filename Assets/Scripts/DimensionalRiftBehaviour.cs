using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionalRiftBehaviour : MonoBehaviour
{
    public GameObject globalController;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            var gController = globalController.GetComponent<GlobalSettings>();
            gController.AddSavePoint(transform.position);
        }
    }
}
