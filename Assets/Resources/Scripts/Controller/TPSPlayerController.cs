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

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
        RunCharacter();
    }

    private void FixedUpdate()
    {
        PerformJump();
    }

    // 대룞 낅젰泥섎━⑸땲
    private void HandleMovementInput()
    {
        if (anim.GetBool("isJumping"))
        {
            return;
        }
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMoving = moveInput.magnitude > 0;
        anim.SetBool("isWalking", isMoving);

        //Vector3 moveDirection = CalculateMoveDirection(moveInput);
        //moveDirection.y = 0f;
        //if (isMoving)
        //{
        //    MoveCharacter(moveDirection);
        //}
        //RotateCharacter(isMoving ? moveDirection : camera.forward);
        if (isMoving)
        {
            Vector3 moveDirection = CalculateMoveDirection(moveInput);
            MoveCharacter(moveDirection);
            RotateCharacter(moveDirection);

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
        }
        if (IsGrounded() && anim.GetBool("isJumping"))
        {
            anim.SetBool("isJumping", false); // 罹먮┃곌 낆뿉 우쑝硫먰봽 곹깭 댁젣
        }
    }

    // 대룞 諛⑺뼢怨꾩궛⑸땲
    private Vector3 CalculateMoveDirection(Vector2 input)
    {
        Vector3 forward = ViewCamera.forward.FlattenVector();
        Vector3 right = ViewCamera.right.FlattenVector();
        return (forward * input.y + right * input.x).normalized;
    }

    // 罹먮┃곕 대룞쒗궢덈떎.
    private void MoveCharacter(Vector3 direction)
    {
        transform.position += direction * movementSpeed * Time.deltaTime;
    }

    private void RunCharacter()
    {
        
            if (Input.GetKey(KeyCode.LeftShift))
        {
            // 踰꾩옱 諛⑺뼢쇰줈 鍮좊Ⅴ寃대룞⑸땲
            Vector3 direction = CalculateMoveDirection(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
            transform.position += direction * Run * Time.deltaTime;
                anim.SetBool("isRuning", true);
        }
        else
        {
            if(Input.GetKeyUp(KeyCode.LeftShift))
            anim.SetBool("isRuning", false);
        }
    }

    // 罹먮┃곕 뚯쟾쒗궢덈떎.
    private void RotateCharacter(Vector3 direction)
    {
        direction.y = 0f; // 뚮젅댁뼱癒몃━諛뺢퀬띠쑝硫쒖꽦
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Character.rotation = Quaternion.Slerp(Character.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //character.rotation = targetRotation;
    }

    // 罹먮┃곌 諛붾떏덈뒗吏 뺤씤⑸땲
    private bool IsGrounded()
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

