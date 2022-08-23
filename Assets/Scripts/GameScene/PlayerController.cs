using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
     public float speed  = 6f;
     public float gravity = 20f;
     public float jumpForce = 8f;
     CharacterController characterController;
     Vector3 moveDir = Vector3.zero;

    private void Awake()
    {

        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {

        if (characterController.isGrounded )
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;
            if (Input.GetButton("Jump"))
                moveDir.y = jumpForce;
        }

        moveDir.y -= gravity * Time.deltaTime;
        characterController.Move(moveDir * Time.deltaTime);
    }
    //public float speed = 6.0F;
    //public float jumpSpeed = 8.0F;
    //public float gravity = 20.0F;
    //private Vector3 moveDirection = Vector3.zero;
    //CharacterController controller ;
    //private void Awake()
    //{
    //    controller = GetComponent<CharacterController>();
    //}
    //void Update()
    //{
    //    if (controller.isGrounded)
    //    {

    //        if (Input.GetButton("Jump"))
    //            moveDirection.y = jumpSpeed;

    //    }
    //    moveDirection.y -= gravity * Time.deltaTime;
    //    controller.Move(moveDirection * Time.deltaTime);
    //}
}
