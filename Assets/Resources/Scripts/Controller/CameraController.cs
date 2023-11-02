using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target; // 플레이어의 Transform
    [SerializeField] private Vector3 offset; // 카메라와 플레이어 간의 오프셋
    [SerializeField] private float pitch = 2.0f; // 카메라의 상하 회전 각도
    [SerializeField] private float zoomSpeed = 4.0f;
    [SerializeField] private float minZoom = 5.0f;
    [SerializeField] private float maxZoom = 15.0f;
    [SerializeField] private float yawSpeed = 100.0f;

    private Vector2 cameraMoveInput;
    private float zoomInput;
    private float currentZoom = 10.0f;
    private float currentYaw = 0.0f;

    public void OnCameraMove(InputAction.CallbackContext _context)
    {
        cameraMoveInput = _context.ReadValue<Vector2>();
        currentYaw -= cameraMoveInput.x * yawSpeed * Time.deltaTime;
    }

    public void OnCameraZoom(InputAction.CallbackContext _context)
    {
        Vector2 zoomInput = _context.ReadValue<Vector2>();
        float zoomValue = zoomInput.y;  // 스크롤 휠의 위/아래 움직임을 사용
        
        currentZoom -= zoomValue * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }

    private void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }

}
