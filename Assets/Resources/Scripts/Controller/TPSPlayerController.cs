using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TPSPlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer; // ë°”ë‹¥ë³„˜ê¸° „í•œ ˆì´ë§ˆìŠ¤
    [SerializeField] private Transform Character;
    [SerializeField] private Transform ViewCamera;
    [SerializeField] private float movementSpeed = 5f; // ´ë™ ë„
    [SerializeField] private float rotationSpeed = 10f; // Œì „ ë„
    [SerializeField] private float jumpPower = 5f; // í”„ 
    [SerializeField] private float distanceToGround = 0.2f; // ë°”ë‹¥ê³¼ì˜ ê±°ë¦¬
    [SerializeField] private float Run = 7f;

  
    private Animator anim;
    private Rigidbody rb;
    private bool _jump;

    private const string JumpButton = "Jump"; // í”„ ë²„íŠ¼

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

    // ´ë™ …ë ¥ì²˜ë¦¬©ë‹ˆ
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

    // í”„ …ë ¥ì²˜ë¦¬©ë‹ˆ
    private void HandleJumpInput()
    {
        if (Input.GetButtonDown(JumpButton) && IsGrounded())
        {
            _jump = true;
            anim.SetBool("isJumping", true); // í”„ œì‘  ë‹ˆë©”ì´œì„±
        }
    }

    // ë¬¼ë¦¬ ê¸°ë°˜ í”„ë¥˜í–‰©ë‹ˆ
    private void PerformJump()
    {
        if (_jump)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            _jump = false;
        }
        if (IsGrounded() && anim.GetBool("isJumping"))
        {
            anim.SetBool("isJumping", false); // ìºë¦­°ê …ì— ¿ìœ¼ë©í”„ íƒœ ´ì œ
        }
    }

    // ´ë™ ë°©í–¥ê³„ì‚°©ë‹ˆ
    private Vector3 CalculateMoveDirection(Vector2 input)
    {
        Vector3 forward = ViewCamera.forward.FlattenVector();
        Vector3 right = ViewCamera.right.FlattenVector();
        return (forward * input.y + right * input.x).normalized;
    }

    // ìºë¦­°ë ´ë™œí‚µˆë‹¤.
    private void MoveCharacter(Vector3 direction)
    {
        transform.position += direction * movementSpeed * Time.deltaTime;
    }

    private void RunCharacter()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // ë²„ì¬ ë°©í–¥¼ë¡œ ë¹ ë¥´ê²´ë™©ë‹ˆ
            Vector3 direction = CalculateMoveDirection(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
            transform.position += direction * Run * Time.deltaTime;
        }
    }

    // ìºë¦­°ë Œì „œí‚µˆë‹¤.
    private void RotateCharacter(Vector3 direction)
    {
        direction.y = 0f; // Œë ˆ´ì–´ë¨¸ë¦¬ë°•ê³ ¶ìœ¼ë©œì„±
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Character.rotation = Quaternion.Slerp(Character.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //character.rotation = targetRotation;
    }

    // ìºë¦­°ê ë°”ë‹¥ˆëŠ”ì§€ •ì¸©ë‹ˆ
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround, groundLayer);
    }
}

public static class VectorExtensions
{
    // ë²¡í„°ë¥‰íƒ„”í•©ˆë‹¤. (Yì¶ê°’ì„ œê±°)
    public static Vector3 FlattenVector(this Vector3 vector)
    {
        return new Vector3(vector.x, 0f, vector.z).normalized;
    }
}

