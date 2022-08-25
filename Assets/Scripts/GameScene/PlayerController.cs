using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float   speed;            // 플레이어 속도
    private float   gravity;          // 중력계수
    private float   jumpForce;        // 점프 힘
    private float   rotateSpeed;      // 회전 속도

    private bool    isWalk;
    private bool    isRun;
    private bool    isJump;

    private Vector3 moveDir;        // 이동 벡터
    private Vector3 mouseRot;       // 회전 벡터

    private Animator            animator;               // 플레이어 애니메이터
    private CharacterController characterController;    // 플레이어 캐릭터컨트롤러
    private AudioSource         playerAudioSource;      // 플레이어 오디오소스
    private CameraController    cameraController;       // 플레이어 오디오소스

    [SerializeField] private AudioClip           playerAudioWalkClip;       // 플레이어 오디오 클립
    [SerializeField] private AudioClip           playerAudioRunClip;        // 플레이어 오디오 클립
    [SerializeField] private AudioClip           playerAudioJumpClip;       // 플레이어 오디오 클립

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
        // 캐릭터가 땅에 닿았을 때.
        if (characterController.isGrounded)
        {
            float xAxis = Input.GetAxis("Horizontal");
            float zAxis = Input.GetAxis("Vertical");

            // 스피드 값은 isRun이 트루이면 12 아니면 6이다.
            speed = isRun ? 8 : 4;

            // new Vector3 -> 월드좌표 // transform.direction -> 로컬좌표 
            // 즉, 로컬좌표로 값을 곱해줌으로써 Move를 로컬좌표 방향으로 사용한다.
            moveDir = (transform.right * xAxis + transform.forward *zAxis).normalized;
            moveDir *= speed;


            // 캐릭터가 땅에 닿았을 때 점프가 가능하다.
            Jump();
            Run();

            // 애니메이션 체크
            isWalk = xAxis == 0 && zAxis == 0 ? false : true;
            if (xAxis != 0 || zAxis != 0)
            {
                playerAudioSource.clip = isRun ? playerAudioRunClip : playerAudioWalkClip;
            }
        }

        // 중력 적용
        moveDir.y -= gravity * Time.deltaTime;

        // 캐릭터 움직임 
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
