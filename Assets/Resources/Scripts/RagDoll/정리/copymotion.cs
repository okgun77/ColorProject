using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copymotion : MonoBehaviour
{
    //[SerializeField] public bool Test;

    public Transform animationObject; // 애니메이션용 객체의 Transform
    public Transform ragdollObject;   // 레그돌 객체의 Transform

    private Vector3 initialPositionOffset; // 초기 위치 오프셋
    private Quaternion initialRotationOffset; // 초기 회전 오프셋

    private Rigidbody Rb;

    public GetTransform getTr = null;

    private bool isInit = false;

    private bool isCoroutineRunning = false; // 코루틴 실행 상태 추적
    private Coroutine ragdollCoroutine; // 실행 중인 코루틴 참조
    //void Start()
    //{
    //    Rb = GetComponent<Rigidbody>();
    //    if (!Test) 
    //    { 
    //    RePlacetarget();

    //    }

    //}

    private void Start()
    {
        Init();
        
    }

    private void OnInit()
    {
        isInit = true;
    }

    private void Update()
    {

    }

    private void RePlacetarget()
    {
        initialPositionOffset = ragdollObject.position - animationObject.position;
        initialRotationOffset = Quaternion.Inverse(animationObject.rotation) * ragdollObject.rotation;
        
    }
   
    public void Init()
    {
        RePlacetarget();
        ragdollCoroutine = StartCoroutine(UpdateRagdollpostion()); // 코루틴 저장
        isCoroutineRunning = true;
    }

    private IEnumerator UpdateRagdollpostion()
    {
        yield return null;
        while (isCoroutineRunning)
        {
            //Debug.Log("animation" + animationObject.position);
            //Debug.Log("ragdoll" + ragdollObject.position);
            //ragdollObject.localPosition = animationObject.localPosition;
            //ragdollObject.localRotation = animationObject.localRotation;
            ragdollObject.position = animationObject.position + initialPositionOffset;
            ragdollObject.rotation = animationObject.rotation * initialRotationOffset;
            yield return null;
        }
    }
    public void PauseCoroutine()
    {
        if (isCoroutineRunning)
        {
            StopCoroutine(ragdollCoroutine); // 코루틴 중지
            isCoroutineRunning = false;
        }
    }

    public void ResumeCoroutine()
    {
        if (!isCoroutineRunning)
        {
            ragdollCoroutine = StartCoroutine(UpdateRagdollpostion()); // 코루틴 재시작
            isCoroutineRunning = true;
        }
    }
}
