using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController playerController;

    private float distance = 0;
    private Vector3 offsetPos = Vector3.zero;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        OffsetCam();
        //RotateCam();
    }

    void OffsetCam()
    {
        distance = Vector3.Distance(transform.position, playerController.transform.position);
        offsetPos = (playerController.transform.position - transform.position).normalized;


        transform.position = playerController.transform.position + (offsetPos * distance);
    }
    void RotateCam()
    { 
        transform.rotation = playerController.transform.rotation;
    }
}
