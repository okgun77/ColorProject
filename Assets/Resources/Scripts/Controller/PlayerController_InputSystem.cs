using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_InputSystem : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private float runMultiplier = 2.0f; // 달리기 속도 배율
    [SerializeField]
    private float jumpForce = 5.0f;

    private Vector2 moveInput;
    private bool isRunning = false;
    private bool isGrounded; // 땅에 닿았는지 여부
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext _context)
    {
        moveInput = _context.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext _context)
    {
        isRunning = _context.ReadValue<float>() > 0.5f; // 달리기 버튼이 눌렸는지 여부
    }

    public void OnJump(InputAction.CallbackContext _context)
    {
        if (isGrounded && _context.started)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        float speed = (isRunning) ? moveSpeed * runMultiplier : moveSpeed;
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * speed;

        // 플레이어의 방향을 설정
        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5.0f);
        }

        transform.position += move * Time.fixedDeltaTime;

        // 땅에 닿았는지 여부 확인
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}