
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TPSPlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer; // 바닥 �별�한 LayerMask
    [SerializeField] private Transform Character;
    [SerializeField] private Transform ViewCamera;
    [SerializeField] private float movementSpeed = 5f; // �동 �도
    [SerializeField] private float rotationSpeed = 10f; // �전 �도
    [SerializeField] private float jumpPower = 5f; // �프 
    [SerializeField] private float distanceToGround = 0.2f; // 바닥과의 거리
    [SerializeField] private float Run = 7f;

  
    private Animator anim;
    private Rigidbody rb;
    private bool _jump;

    private const string JumpButton = "Jump"; // �프 버튼


    //Sound
    public string WalkSound;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
        HandleMovementInput();
        HandleJumpInput();

        //if (isMoving)
        //{
        //    // �직임감�면 �직임 방향계산�고 캐릭�� �직입�다.
        //    Vector3 moveDirection = CalculateMoveDirection(moveInput);
        //    MoveCharacter(moveDirection);
        //    WalkSound = SoundManager.Instance.PlayerWalkSound;
        //    SoundManager.Instance.PlaySE(WalkSound);
        //}
        //else
        //{
        //    // �직임�을 경우 �요처리륩니( 걷는 �리 �� .
        //    WalkSound = SoundManager.Instance.PlayerWalkSound;
        //    SoundManager.Instance.StopSE(WalkSound);
        //}

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("LeftShift pressed");
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log("LeftShift released");
        }
       
    }

    private void FixedUpdate()
    {
        PerformJump();
    }

    
    private void HandleMovementInput()
    {
        if (anim.GetBool("isJumping"))
        {
            return;
        }

        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMoving = moveInput.magnitude > 0;
        anim.SetBool("isWalking", isMoving);

        if (isMoving)
        {
            Vector3 moveDirection = CalculateMoveDirection(moveInput);
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float currentSpeed = isRunning ? Run : movementSpeed;

            MoveCharacter(moveDirection, currentSpeed);
            RotateCharacter(moveDirection);

            string currentSound = isRunning ? SoundManager.Instance.PlayerRightWalk : SoundManager.Instance.PlayerLeftWalk;

            
           // if (!SoundManager.Instance.IsPlaying(currentSound))
            //{
           //     SoundManager.Instance.PlaySE(currentSound);
           // }

            anim.SetBool("isRunning", isRunning);
        }
        else
        {
            
           // SoundManager.Instance.StopSE(SoundManager.Instance.PlayerRightWalk);
            //SoundManager.Instance.StopSE(SoundManager.Instance.PlayerLeftWalk);
            anim.SetBool("isRunning", false);
        }
    }

    // �프 �력 처리
    private void HandleJumpInput()
    {
        if (Input.GetButtonDown(JumpButton) && IsGrounded())
        {
            _jump = true;
            anim.SetBool("isJumping", true); // 먰봽 �옉 좊땲硫붿�쒖�
        }

    }

    
    // 물리 기반 �프 �행
    private void PerformJump()
    {
        if (_jump)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            _jump = false;
            anim.SetBool("isJumping", true); // �프륜작�니메이�태륤정
        }
        else
        {
            anim.SetBool("isJumping", false); // 캐릭�� �에 �으멐프 �니메이�태르제
        }

    }

   
    private Vector3 CalculateMoveDirection(Vector2 input)
    {
        Vector3 forward = ViewCamera.forward.FlattenVector();
        Vector3 right = ViewCamera.right.FlattenVector();
        return (forward * input.y + right * input.x).normalized;
    }

   
    private void MoveCharacter(Vector3 direction, float speed)
    {
        transform.position += direction * speed * Time.deltaTime;
    }

  

    
    private void RotateCharacter(Vector3 direction)
    {
        direction.y = 0f; 
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Character.rotation = Quaternion.Slerp(Character.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //character.rotation = targetRotation;
    }

   
    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround, groundLayer);
    }
}

public static class VectorExtensions
{
    // 벡터 �탄Y�개거)
    public static Vector3 FlattenVector(this Vector3 vector)
    {
        return new Vector3(vector.x, 0f, vector.z).normalized;
    }
   

}

