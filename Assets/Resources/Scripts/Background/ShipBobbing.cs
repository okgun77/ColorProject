using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBobbing : MonoBehaviour
{
    [SerializeField] private float tiltAngle = 5.0f; // 최대 기울기 각도
    [SerializeField] private float tiltSpeed = 1.5f; // 기울기 변화 속도

    // 주기적으로 변화하는 각도를 위한 변수
    private float timeCounter = 0.0f;

    void Update()
    {
        // 시간에 따라 변화하는 값 계산
        timeCounter += Time.deltaTime * tiltSpeed;

        // Mathf.Sin과 Mathf.Cos를 사용하여 파도에 의한 기울기를 계산
        float tiltX = Mathf.Sin(timeCounter) * tiltAngle;
        float tiltZ = Mathf.Cos(timeCounter) * tiltAngle;

        // 기존의 배의 회전(Quaternion.Euler)에 더하여 새로운 기울기를 적용
        Quaternion targetRotation = Quaternion.Euler(tiltX, 0, tiltZ);
        transform.rotation = targetRotation;
    }
}
