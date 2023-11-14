using UnityEngine;

public class ObjectRotator2 : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 20.0f; // 회전 속도
    [SerializeField] private float maxRotationAngle = 45.0f; // 최대 회전 각도
    [SerializeField] private Vector3 rotationAxis = Vector3.up; // 회전 축
    [SerializeField] private Rigidbody rb; // 회전에 사용될 Rigidbody

    private float currentRotationAngle = 0f; // 현재 회전 각도
    private bool isReversing = false; // 회전 방향 반전 여부

    void Start()
    {
        rb.isKinematic = false; // 물리 엔진의 영향을 받게 함
    }

    void Update()
    {
        // 현재 회전 각도 업데이트
        currentRotationAngle += rotationSpeed * Time.deltaTime;

        // 회전 각도가 최대값을 초과했는지 확인
        if (Mathf.Abs(currentRotationAngle) > maxRotationAngle)
        {
            // 회전 방향 반전
            isReversing = !isReversing;
            currentRotationAngle = maxRotationAngle * (isReversing ? 1 : -1);
        }

        // Rigidbody에 각속도 적용
        rb.angularVelocity = (isReversing ? -1 : 1) * rotationSpeed * Mathf.Deg2Rad * rotationAxis;
    }
}