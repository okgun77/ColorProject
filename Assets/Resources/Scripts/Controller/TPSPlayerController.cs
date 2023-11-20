
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TPSPlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer; // 諛붾떏 앸퀎꾪븳 LayerMask
    [SerializeField] private Transform Character;
    [SerializeField] private Transform ViewCamera;
    [SerializeField] private float movementSpeed = 5f; // 대룞 띾룄
    [SerializeField] private float rotationSpeed = 10f; // 뚯쟾 띾룄
    [SerializeField] private float jumpPower = 5f; // 먰봽 
    [SerializeField] private float distanceToGround = 0.2f; // 諛붾떏怨쇱쓽 嫄곕━
    [SerializeField] private float Run = 7f;

  
    private Animator anim;
    private Rigidbody rb;
    private CopyAnim copyanim;
    private bool _jump;

    private const string JumpButton = "Jump"; // 먰봽 踰꾪듉


    //Sound
    public string WalkSound;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        copyanim = GetComponent<CopyAnim>();
        
    }

    private void Update()
    {
        
        HandleMovementInput();
        HandleJumpInput();

        //if (isMoving)
        //{
        //    // 吏곸엫媛먯섎㈃ 吏곸엫 諛⑺뼢怨꾩궛섍퀬 罹먮┃곕 吏곸엯덈떎.
        //    Vector3 moveDirection = CalculateMoveDirection(moveInput);
        //    MoveCharacter(moveDirection);
        //    WalkSound = SoundManager.Instance.PlayerWalkSound;
        //    SoundManager.Instance.PlaySE(WalkSound);
        //}
        //else
        //{
        //    // 吏곸엫놁쓣 寃쎌슦 꾩슂泥섎━瑜⑸땲( 嫄룸뒗 뚮━ 뺤 .
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

    // 먰봽 낅젰 泥섎━
    private void HandleJumpInput()
    {
        if (Input.GetButtonDown(JumpButton) && IsGrounded())
        {
            _jump = true;
            anim.SetBool("isJumping", true); // 癒곕늄 뽰삂 醫딅빍筌롫뗄좎뮇苑
        }

    }

    
    // 臾쇰━ 湲곕컲 먰봽 섑뻾
    private void PerformJump()
    {
        if (_jump)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            _jump = false;
            anim.SetBool("isJumping", true); // 먰봽瑜쒖옉좊땲硫붿씠곹깭瑜ㅼ젙
        }
        else
        {
            anim.SetBool("isJumping", false); // 罹먮┃곌 낆뿉 우쑝硫먰봽 좊땲硫붿씠곹깭瑜댁젣
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
    // 踰≫꽣 됲깂Y異媛쒓굅)
    public static Vector3 FlattenVector(this Vector3 vector)
    {
        return new Vector3(vector.x, 0f, vector.z).normalized;
    }
   

}

