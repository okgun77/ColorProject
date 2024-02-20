using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copymotion : MonoBehaviour
{
    //[SerializeField] public bool Test;

    public Transform animationObject; // 좊땲硫붿씠섏슜 媛앹껜Transform
    public Transform ragdollObject;   // 덇렇媛앹껜Transform

    private Vector3 initialPositionOffset; // 珥덇린 꾩튂 ㅽ봽
    private Quaternion initialRotationOffset; // 珥덇린 뚯쟾 ㅽ봽

    private Rigidbody Rb;

    public GetTransform getTr = null;

    private bool isInit = false;

    private bool isCoroutineRunning = false; // 肄붾（ㅽ뻾 곹깭 異붿쟻
    private Coroutine ragdollCoroutine; // ㅽ뻾 以묒씤 肄붾（李몄“
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
        ragdollCoroutine = StartCoroutine(UpdateRagdollpostion()); // 肄붾（
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
            StopCoroutine(ragdollCoroutine); // 肄붾（以묒
            isCoroutineRunning = false;
        }
    }

    public void ResumeCoroutine()
    {
        if (!isCoroutineRunning)
        {
            ragdollCoroutine = StartCoroutine(UpdateRagdollpostion()); // 肄붾（ъ떆
            isCoroutineRunning = true;
        }
    }
}
