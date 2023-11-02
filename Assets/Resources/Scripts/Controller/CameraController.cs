using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target; // �÷��̾��� Transform
    [SerializeField] private Vector3 offset; // ī�޶�� �÷��̾� ���� ������
    [SerializeField] private float pitch = 2.0f; // ī�޶��� ���� ȸ�� ����
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
        float zoomValue = zoomInput.y;  // ��ũ�� ���� ��/�Ʒ� �������� ���
        
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
