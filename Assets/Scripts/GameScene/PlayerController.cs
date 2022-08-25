using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float   speed;            // �÷��̾� �ӵ�
    private float   gravity;          // �߷°��
    private float   jumpForce;        // ���� ��
    private float   rotateSpeed;      // ȸ�� �ӵ�

    private bool    isWalk;
    private bool    isRun;
    private bool    isJump;

    private Vector3 moveDir;        // �̵� ����
    private Vector3 mouseRot;       // ȸ�� ����

    private Animator            animator;               // �÷��̾� �ִϸ�����
    private CharacterController characterController;    // �÷��̾� ĳ������Ʈ�ѷ�
    private AudioSource         playerAudioSource;      // �÷��̾� ������ҽ�
    private CameraController    cameraController;       // �÷��̾� ������ҽ�

    [SerializeField] private AudioClip           playerAudioWalkClip;       // �÷��̾� ����� Ŭ��
    [SerializeField] private AudioClip           playerAudioRunClip;        // �÷��̾� ����� Ŭ��
    [SerializeField] private AudioClip           playerAudioJumpClip;       // �÷��̾� ����� Ŭ��

    private float limitMinX = -80;
    private float limitMaxX = 50;
    private float eulerAngleX;
    private float eulerAngleY;

    private void Awake()
    {

        characterController = GetComponent<CharacterController>();
        animator            = GetComponentInChildren<Animator>();
        playerAudioSource   = GetComponentInChildren<AudioSource>();
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        speed       = 6f;
        gravity     = 20f;
        jumpForce   = 8f;
        rotateSpeed = 2f;
        moveDir     = Vector3.zero;
        mouseRot    = Vector3.zero;
        isWalk = false;
        isRun = false;
    }

    void Update()
    {
        Move();
        Rotation();
        Animation();
    }

    void Move()
    {
        // ĳ���Ͱ� ���� ����� ��.
        if (characterController.isGrounded)
        {
            float xAxis = Input.GetAxis("Horizontal");
            float zAxis = Input.GetAxis("Vertical");

            // ���ǵ� ���� isRun�� Ʈ���̸� 12 �ƴϸ� 6�̴�.
            speed = isRun ? 8 : 4;

            // new Vector3 -> ������ǥ // transform.direction -> ������ǥ 
            // ��, ������ǥ�� ���� ���������ν� Move�� ������ǥ �������� ����Ѵ�.
            moveDir = (transform.right * xAxis + transform.forward *zAxis).normalized;
            moveDir *= speed;


            // ĳ���Ͱ� ���� ����� �� ������ �����ϴ�.
            Jump();
            Run();

            // �ִϸ��̼� üũ
            isWalk = xAxis == 0 && zAxis == 0 ? false : true;
            if (xAxis != 0 || zAxis != 0)
            {
                playerAudioSource.clip = isRun ? playerAudioRunClip : playerAudioWalkClip;
            }
        }

        // �߷� ����
        moveDir.y -= gravity * Time.deltaTime;

        // ĳ���� ������ 
        characterController.Move(moveDir * Time.deltaTime);
        
    }
    void Rotation()
    {
        float mouseXAxis = Input.GetAxis("Mouse X");
        float mouseYAxis = Input.GetAxis("Mouse Y");

        eulerAngleY += mouseXAxis * 3f;
        eulerAngleX -= mouseYAxis * 3f;

        eulerAngleX = Mathf.Clamp(eulerAngleX,limitMinX,limitMaxX);

        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
    }


    void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            moveDir.y = jumpForce;
            isJump = true;
        }
        else isJump = false;
    }
    void Run()
    {
        isRun = Input.GetKey(KeyCode.LeftShift) ? true : false;


    }

    void Animation()
    {
        animator.SetBool("IsWalk", isWalk);
        animator.SetBool("IsRun", isRun);
    }



    void playSound(AudioClip audioClip)
    {
        playerAudioSource.Stop();
        playerAudioSource.clip = audioClip;
        
    }
}
