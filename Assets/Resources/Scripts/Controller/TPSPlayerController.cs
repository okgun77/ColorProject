
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TPSPlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer; // 諛붾떏앸퀎섍린 꾪븳 덉씠留덉뒪
    [SerializeField] private Transform Character;
    [SerializeField] private Transform ViewCamera;
    [SerializeField] private float movementSpeed = 5f; // 대룞 띾룄
    [SerializeField] private float rotationSpeed = 10f; // 뚯쟾 띾룄
    [SerializeField] private float jumpPower = 5f; // 먰봽 
    [SerializeField] private float distanceToGround = 0.2f; // 諛붾떏怨쇱쓽 嫄곕━
    [SerializeField] private float Run = 7f;

  
    private Animator anim;
    private Rigidbody rb;
    private bool _jump;

    private const string JumpButton = "Jump"; // 먰봽 踰꾪듉


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
        //    // 움직임이 감지되면 움직임 방향을 계산하고 캐릭터를 움직입니다.
        //    Vector3 moveDirection = CalculateMoveDirection(moveInput);
        //    MoveCharacter(moveDirection);
        //    WalkSound = SoundManager.Instance.PlayerWalkSound;
        //    SoundManager.Instance.PlaySE(WalkSound);
        //}
        //else
        //{
        //    // 움직임이 없을 경우 필요한 처리를 합니다 (예: 걷는 소리 정지 등).
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
            float currentSpeed = isRunning ? Run : movementSpeed; // LeftShift가 눌려있으면 Run 속도, 아니면 기본 이동 속도 사용

            MoveCharacter(moveDirection, currentSpeed);
            RotateCharacter(moveDirection);

            anim.SetBool("isRunning", isRunning); // 달리기 상태 애니메이션 업데이트
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    // 먰봽 낅젰泥섎━⑸땲
    private void HandleJumpInput()
    {
        if (Input.GetButtonDown(JumpButton) && IsGrounded())
        {
            _jump = true;
            anim.SetBool("isJumping", true); // 먰봽 쒖옉 좊땲硫붿씠쒖꽦
        }

    }

    
    // 臾쇰━ 湲곕컲 먰봽瑜섑뻾⑸땲
    private void PerformJump()
    {
        if (_jump)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            _jump = false;
            anim.SetBool("isJumping", true); // 점프를 시작할 때 애니메이션 상태를 설정
        }
        else
        {
            anim.SetBool("isJumping", false); // 캐릭터가 땅에 닿으면 점프 애니메이션 상태를 해제
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
    // 踰≫꽣瑜됲깂뷀빀덈떎. (Y異媛믪쓣 쒓굅)
    public static Vector3 FlattenVector(this Vector3 vector)
    {
        return new Vector3(vector.x, 0f, vector.z).normalized;
    }
   

}

