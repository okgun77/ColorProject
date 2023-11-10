using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TPSPlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer; // �ٴ��� �ĺ��ϱ� ���� ���̾� ����ũ.
    [SerializeField] private Transform Character;
    [SerializeField] private Transform ViewCamera;
    [SerializeField] private float movementSpeed = 5f; // �̵� �ӵ�
    [SerializeField] private float rotationSpeed = 10f; // ȸ�� �ӵ�
    [SerializeField] private float jumpPower = 5f; // ���� ��
    [SerializeField] private float distanceToGround = 0.2f; // �ٴڰ��� �Ÿ�
    [SerializeField] private float Run = 7f;

  
    private Animator anim;
    private Rigidbody rb;
    private bool _jump;

    private const string JumpButton = "Jump"; // ���� ��ư

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

    // �̵� �Է��� ó���մϴ�.
    private void HandleMovementInput()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMoving = moveInput.magnitude > 0;
        // anim.SetBool("isMove", isMoving);

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

    // ���� �Է��� ó���մϴ�.
    private void HandleJumpInput()
    {
        if (Input.GetButtonDown(JumpButton) && IsGrounded())
        {
            _jump = true;
        }
    }

    // ���� ��� ������ �����մϴ�.
    private void PerformJump()
    {
        if (_jump)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            _jump = false;
        }
    }

    // �̵� ������ ����մϴ�.
    private Vector3 CalculateMoveDirection(Vector2 input)
    {
        Vector3 forward = ViewCamera.forward.FlattenVector();
        Vector3 right = ViewCamera.right.FlattenVector();
        return (forward * input.y + right * input.x).normalized;
    }

    // ĳ���͸� �̵���ŵ�ϴ�.
    private void MoveCharacter(Vector3 direction)
    {
        transform.position += direction * movementSpeed * Time.deltaTime;
    }

    private void RunCharacter()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // �� �� �� ���� �������� ������ �̵��մϴ�.
            Vector3 direction = CalculateMoveDirection(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
            transform.position += direction * Run * Time.deltaTime;
        }
    }

    // ĳ���͸� ȸ����ŵ�ϴ�.
    private void RotateCharacter(Vector3 direction)
    {
        direction.y = 0f; // �÷��̾ �Ӹ��ڰ������ Ȱ��ȭ
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Character.rotation = Quaternion.Slerp(Character.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //character.rotation = targetRotation;
    }

    // ĳ���Ͱ� �ٴڿ� �ִ��� Ȯ���մϴ�.
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround, groundLayer);
    }
}

public static class VectorExtensions
{
    // ���͸� ��źȭ�մϴ�. (Y�� ���� ����)
    public static Vector3 FlattenVector(this Vector3 vector)
    {
        return new Vector3(vector.x, 0f, vector.z).normalized;
    }
}

