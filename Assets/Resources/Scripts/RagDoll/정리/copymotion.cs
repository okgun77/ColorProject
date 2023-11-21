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

    //void Start()
    //{
    //    Rb = GetComponent<Rigidbody>();
    //    if (!Test) 
    //    { 
    //    RePlacetarget();
            
    //    }
        
    //}

   
    void Update()
    {
        //if (!Test)
        //{
        //    Debug.Log("animation"+ animationObject.position);
        //    Debug.Log("ragdoll" + ragdollObject.position);
        //    ragdollObject.position = animationObject.position + initialPositionOffset;
        //    ragdollObject.rotation = animationObject.rotation * initialRotationOffset;
           

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
        StartCoroutine(UpdateRagdollpostion());
    }

    private IEnumerator UpdateRagdollpostion()
    {
        while (true)
        {
            Debug.Log("animation" + animationObject.position);
            Debug.Log("ragdoll" + ragdollObject.position);
            ragdollObject.position = animationObject.position + initialPositionOffset;
            ragdollObject.rotation = animationObject.rotation * initialRotationOffset;
            yield return null;
        }
    }
}
