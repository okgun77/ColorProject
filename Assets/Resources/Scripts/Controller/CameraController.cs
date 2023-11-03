using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float pitch = 2.0f;
    [SerializeField] private float yawSpeed = 100.0f;
    [SerializeField] private float minZoom = 5.0f;
    [SerializeField] private float maxZoom = 15.0f;
    private float currentZoom = 10.0f;
    private float currentYaw = 0.0f;

    public void OnLook(InputAction.CallbackContext _context)
    {
        Vector2 input = _context.ReadValue<Vector2>();
        currentYaw -= input.x * yawSpeed * Time.deltaTime;
    }

    public void OnZoom(InputAction.CallbackContext _context)
    {
        currentZoom -= _context.ReadValue<float>();
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }

    private void Update()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }
}
