using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    [SerializeField] private float[] rotationSpeeds; // 가능한 회전 속도 배열
    [SerializeField] private float speedChangeTime = 2.0f; // 속도 변경 시간 간격
    [SerializeField] private Rigidbody rb; // 회전에 사용될 Rigidbody
    [SerializeField] private Vector3 rotationAxis = Vector3.up; // 회전 축

    private float nextSpeedChangeTime; // 다음 속도 변경 시점
    private float currentRotationSpeed; // 현재 적용할 회전 속도

    void Start()
    {
        rb.isKinematic = false; // 물리 엔진의 영향을 받게 함
        ChangeRotationSpeed(); // 초기 회전 속도 설정
        nextSpeedChangeTime = Time.time + speedChangeTime; // 다음 속도 변경 시간 설정
    }

    void Update()
    {
        // 시간이 되면 속도 변경
        if (Time.time >= nextSpeedChangeTime)
        {
            ChangeRotationSpeed();
            nextSpeedChangeTime += speedChangeTime; // 다음 속도 변경 시간을 현재 시간에서 지정한 간격만큼 더해 업데이트
        }
    }

    void ChangeRotationSpeed()
    {
        // 새로운 회전 속도를 랜덤하게 선택
        currentRotationSpeed = rotationSpeeds[Random.Range(0, rotationSpeeds.Length)];
        rb.angularVelocity = currentRotationSpeed * Mathf.Deg2Rad * rotationAxis; // 각속도 업데이트
    }
}