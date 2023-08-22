using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    CanvasController controller;
    void Start()
    {
        controller = GameObject.Find("Canvas").GetComponent<CanvasController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            controller.SetCheckPoint(transform);
        }
    }

}
