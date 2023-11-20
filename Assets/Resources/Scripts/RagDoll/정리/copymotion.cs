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
        
        RePlacetarget();
    }

   
    void Update()
    {
        // 애니메이션용 객체의 위치와 회전을 읽어와 레그돌 객체에 적용
        ragdollObject.position = animationObject.position + initialPositionOffset;
        ragdollObject.rotation = animationObject.rotation * initialRotationOffset;
    }

    private void RePlacetarget()
    {
        initialPositionOffset = ragdollObject.position - animationObject.position;
        initialRotationOffset = Quaternion.Inverse(animationObject.rotation) * ragdollObject.rotation;
    }
   
}
