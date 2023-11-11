using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TPSPlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer; // λ°λ₯λ³κΈ° ν μ΄λ§μ€
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

    // ΄λ λ ₯μ²λ¦¬©λ
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

    // ν λ ₯μ²λ¦¬©λ
    private void HandleJumpInput()
    {
        if (Input.GetButtonDown(JumpButton) && IsGrounded())
        {
            _jump = true;
            anim.SetBool("isJumping", true); // ν μ  λλ©μ΄μ±
        }
    }

    // λ¬Όλ¦¬ κΈ°λ° νλ₯ν©λ
    private void PerformJump()
    {
        if (_jump)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            _jump = false;
        }
        if (IsGrounded() && anim.GetBool("isJumping"))
        {
            anim.SetBool("isJumping", false); // μΊλ¦­°κ μ ΏμΌλ©ν ν ΄μ 
        }
    }

    // ΄λ λ°©ν₯κ³μ°©λ
    private Vector3 CalculateMoveDirection(Vector2 input)
    {
        Vector3 forward = ViewCamera.forward.FlattenVector();
        Vector3 right = ViewCamera.right.FlattenVector();
        return (forward * input.y + right * input.x).normalized;
    }

    // μΊλ¦­°λ ΄λν΅λ€.
    private void MoveCharacter(Vector3 direction)
    {
        transform.position += direction * movementSpeed * Time.deltaTime;
    }

    private void RunCharacter()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // λ²μ¬ λ°©ν₯Όλ‘ λΉ λ₯΄κ²΄λ©λ
            Vector3 direction = CalculateMoveDirection(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
            transform.position += direction * Run * Time.deltaTime;
        }
    }

    // μΊλ¦­°λ μ ν΅λ€.
    private void RotateCharacter(Vector3 direction)
    {
        direction.y = 0f; // λ ΄μ΄λ¨Έλ¦¬λ°κ³ ΆμΌλ©μ±
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Character.rotation = Quaternion.Slerp(Character.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //character.rotation = targetRotation;
    }

    // μΊλ¦­°κ λ°λ₯λμ§ μΈ©λ
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround, groundLayer);
    }
}

public static class VectorExtensions
{
    // λ²‘ν°λ₯νν©λ€. (YμΆκ°μ κ±°)
    public static Vector3 FlattenVector(this Vector3 vector)
    {
        return new Vector3(vector.x, 0f, vector.z).normalized;
    }
}

