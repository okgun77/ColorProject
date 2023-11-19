
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TPSPlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer; // λ°λ₯ λ³ν LayerMask
    [SerializeField] private Transform Character;
    [SerializeField] private Transform ViewCamera;
    [SerializeField] private float movementSpeed = 5f; // ΄λ λ
    [SerializeField] private float rotationSpeed = 10f; // μ  λ
    [SerializeField] private float jumpPower = 5f; // ν 
    [SerializeField] private float distanceToGround = 0.2f; // λ°λ₯κ³Όμ κ±°λ¦¬
    [SerializeField] private float Run = 7f;

  
    private Animator anim;
    private Rigidbody rb;
    private bool _jump;

    private const string JumpButton = "Jump"; // ν λ²νΌ


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
        //    // μ§μκ°μλ©΄ μ§μ λ°©ν₯κ³μ°κ³  μΊλ¦­°λ μ§μλ€.
        //    Vector3 moveDirection = CalculateMoveDirection(moveInput);
        //    MoveCharacter(moveDirection);
        //    WalkSound = SoundManager.Instance.PlayerWalkSound;
        //    SoundManager.Instance.PlaySE(WalkSound);
        //}
        //else
        //{
        //    // μ§μμ κ²½μ° μμ²λ¦¬λ₯©λ( κ±·λ λ¦¬ μ .
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

    // ν λ ₯ μ²λ¦¬
    private void HandleJumpInput()
    {
        if (Input.GetButtonDown(JumpButton) && IsGrounded())
        {
            _jump = true;
            anim.SetBool("isJumping", true); // λ¨°λ΄½ μ μ’λ²ο§λΆΏ μκ½
        }

    }

    
    // λ¬Όλ¦¬ κΈ°λ° ν ν
    private void PerformJump()
    {
        if (_jump)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            _jump = false;
            anim.SetBool("isJumping", true); // νλ₯μ λλ©μ΄νλ₯€μ 
        }
        else
        {
            anim.SetBool("isJumping", false); // μΊλ¦­°κ μ ΏμΌλ©ν  λλ©μ΄νλ₯΄μ 
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
    // λ²‘ν° νYμΆκ°κ±°)
    public static Vector3 FlattenVector(this Vector3 vector)
    {
        return new Vector3(vector.x, 0f, vector.z).normalized;
    }
   

}

