using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TPSPlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer; // 바닥을 식별하기 위한 레이어 마스크.
    [SerializeField] private Transform Character;
    [SerializeField] private Transform ViewCamera;
    [SerializeField] private float movementSpeed = 5f; // 이동 속도
    [SerializeField] private float rotationSpeed = 10f; // 회전 속도
    [SerializeField] private float jumpPower = 5f; // 점프 힘
    [SerializeField] private float distanceToGround = 0.2f; // 바닥과의 거리
    [SerializeField] private float Run = 7f;

  
    // private Animator anim;
    private Rigidbody rb;
    private bool _jump;

    private const string JumpButton = "Jump"; // 점프 버튼

    private void Awake()
    {
        // anim = GetComponent<Animator>();
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

    // 이동 입력을 처리합니다.
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

    // 점프 입력을 처리합니다.
    private void HandleJumpInput()
    {
        if (Input.GetButtonDown(JumpButton) && IsGrounded())
        {
            _jump = true;
        }
    }

    // 물리 기반 점프를 수행합니다.
    private void PerformJump()
    {
        if (_jump)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            _jump = false;
        }
    }

    // 이동 방향을 계산합니다.
    private Vector3 CalculateMoveDirection(Vector2 input)
    {
        Vector3 forward = ViewCamera.forward.FlattenVector();
        Vector3 right = ViewCamera.right.FlattenVector();
        return (forward * input.y + right * input.x).normalized;
    }

    // 캐릭터를 이동시킵니다.
    private void MoveCharacter(Vector3 direction)
    {
        transform.position += direction * movementSpeed * Time.deltaTime;
    }

    private void RunCharacter()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // 단 한 번 현재 방향으로 빠르게 이동합니다.
            Vector3 direction = CalculateMoveDirection(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
            transform.position += direction * Run * Time.deltaTime;
        }
    }

    // 캐릭터를 회전시킵니다.
    private void RotateCharacter(Vector3 direction)
    {
        direction.y = 0f; // 플레이어도 머리박고싶으면 활성화
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Character.rotation = Quaternion.Slerp(Character.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //character.rotation = targetRotation;
    }

    // 캐릭터가 바닥에 있는지 확인합니다.
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround, groundLayer);
    }
}

public static class VectorExtensions
{
    // 벡터를 평탄화합니다. (Y축 값을 제거)
    public static Vector3 FlattenVector(this Vector3 vector)
    {
        return new Vector3(vector.x, 0f, vector.z).normalized;
    }
}

