using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    public Rigidbody[] ragdollParts; // 레그돌 부분에 대한 참조

    private void Start()
    {
        SetRagdollState(true); // 시작 시 레그돌 활성화
    }

    public void SetRagdollState(bool state)
    {
        foreach (var part in ragdollParts)
        {
            part.isKinematic = !state; // 레그돌 상태 설정
        }
    }
}
