using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController playerController;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        OffsetCam();
        RotateCam();
    }

    void OffsetCam()
    { 
        Vector3 pos = playerController.transform.position;
        pos.y += 1f;
        transform.position = pos;
    }
    void RotateCam()
    { 
        transform.rotation = playerController.transform.rotation;
    }
}
