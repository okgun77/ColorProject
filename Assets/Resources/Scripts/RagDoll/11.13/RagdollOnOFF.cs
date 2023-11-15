using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollOnOFF : MonoBehaviour
{
    public BoxCollider mainCollider; // 전체 캐릭터 애니메이터
    public GameObject PlayerRagdollOj;
    public Animator playerAnimatorOj;

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
        playerAnimatorOj.enabled = false;
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
        
        mainCollider.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
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
        playerAnimatorOj.enabled = true;
        mainCollider.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    void GetRagdollBits()
    {
        ragdollColliders = PlayerRagdollOj.GetComponentsInChildren<Collider>();
        ragdollRigidbodies = PlayerRagdollOj.GetComponentsInChildren<Rigidbody>();
    }
}
