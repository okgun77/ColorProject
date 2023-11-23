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
        //Invoke("OnInit", 1f);
    }

    private void OnInit()
    {
        isInit = true;
    }


    void Update()
    {
        //if (!Test)
        //{
        //    Debug.Log("animation"+ animationObject.position);
        //    Debug.Log("ragdoll" + ragdollObject.position);
        //if (isInit)
        // {
        ragdollObject.localPosition = animationObject.localPosition;// + initialPositionOffset;
        ragdollObject.localRotation = animationObject.localRotation;// * initialRotationOffset;
       // }
           

        //}
        
    }

    private void RePlacetarget()
    {
        initialPositionOffset = ragdollObject.position - animationObject.position;
        initialRotationOffset = Quaternion.Inverse(animationObject.rotation) * ragdollObject.rotation;
        
    }
   
    public void Init()
    {
        RePlacetarget();
        //StartCoroutine(UpdateRagdollpostion());
    }

    private IEnumerator UpdateRagdollpostion()
    {
        while (true)
        {
            //Debug.Log("animation" + animationObject.position);
            //Debug.Log("ragdoll" + ragdollObject.position);
            ragdollObject.position = animationObject.position + initialPositionOffset;
            ragdollObject.rotation = animationObject.rotation * initialRotationOffset;
            yield return null;
        }
    }
}
