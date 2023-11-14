using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollOnOFF : MonoBehaviour
{
    public Animator ThisGuysAnimator; // 전체 캐릭터 애니메이터
    public Transform ragdollPart; // 레그돌을 적용할 부위의 Transform

    private Collider[] ragdollColliders;
    private Rigidbody[] ragdollRigidbodies;

    private void Start()
    {
        GetRagdollBits();
        RagdollModeOff(); // 시작 시 애니메이션 모드 활성화
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "KnockDownCube")
        {
            RagdollModeOn();
        }
    }

    void RagdollModeOn()
    {
        // 레그돌 활성화
        foreach (Collider col in ragdollColliders)
        {
            col.enabled = true;
        }

        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = false;
        }

        // 해당 부위의 애니메이션 비활성화
        ThisGuysAnimator.enabled = false;
    }

    void RagdollModeOff()
    {
        // 레그돌 비활성화
        foreach (Collider col in ragdollColliders)
        {
            col.enabled = false;
        }

        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }

        // 애니메이션 활성화
        ThisGuysAnimator.enabled = true;
    }

    void GetRagdollBits()
    {
        ragdollColliders = ragdollPart.GetComponentsInChildren<Collider>();
        ragdollRigidbodies = ragdollPart.GetComponentsInChildren<Rigidbody>();
    }
}
