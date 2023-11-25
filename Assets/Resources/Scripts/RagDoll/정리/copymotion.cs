using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copymotion : MonoBehaviour
{
    //[SerializeField] public bool Test;

    public Transform animationObject; // �니메이�용 객체Transform
    public Transform ragdollObject;   // �그객체Transform

    private Vector3 initialPositionOffset; // 초기 �치 �프
    private Quaternion initialRotationOffset; // 초기 �전 �프

    private Rigidbody Rb;

    public GetTransform getTr = null;

    private bool isInit = false;

    private bool isCoroutineRunning = false; // 코루�행 �태 추적
    private Coroutine ragdollCoroutine; // �행 중인 코루참조
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
        ragdollCoroutine = StartCoroutine(UpdateRagdollpostion()); // 코루�
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
            StopCoroutine(ragdollCoroutine); // 코루중�
            isCoroutineRunning = false;
        }
    }

    public void ResumeCoroutine()
    {
        if (!isCoroutineRunning)
        {
            ragdollCoroutine = StartCoroutine(UpdateRagdollpostion()); // 코루�시
            isCoroutineRunning = true;
        }
    }
}
