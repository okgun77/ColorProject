using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copymotion : MonoBehaviour
{
    public Transform animationObject; // 애니메이션용 객체의 Transform
    public Transform ragdollObject;   // 레그돌 객체의 Transform

    private Vector3 initialPositionOffset; // 초기 위치 오프셋
    private Quaternion initialRotationOffset; // 초기 회전 오프셋

    void Start()
    {
        // 초기 위치 및 회전 오프셋 계산
        initialPositionOffset = ragdollObject.position - animationObject.position;
        initialRotationOffset = Quaternion.Inverse(animationObject.rotation) * ragdollObject.rotation;
    }

    // Update 함수에서 레그돌 객체의 움직임을 애니메이션용 객체의 움직임에 따라 적용
    void Update()
    {
        // 애니메이션용 객체의 위치와 회전을 읽어와 레그돌 객체에 적용
        ragdollObject.position = animationObject.position + initialPositionOffset;
        ragdollObject.rotation = animationObject.rotation * initialRotationOffset;
    }
}
