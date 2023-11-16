using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyModel : MonoBehaviour
{
    [SerializeField] private GameObject src = null;
    [SerializeField] private GameObject dest = null;

    void Start()
    {
        if (src == null || dest == null) return;

        // Rigidbody 복사
        CopyRigidbodies();

        // Joint 복사
        CopyJoints();

        // Collider 복사
        CopyColliders();
    }

    void CopyRigidbodies()
    {
        Rigidbody[] srcRigidbodies = src.GetComponentsInChildren<Rigidbody>();
        Rigidbody[] destRigidbodies = dest.GetComponentsInChildren<Rigidbody>();

        for (int i = 0; i < srcRigidbodies.Length; ++i)
        {
            Rigidbody srcRb = srcRigidbodies[i];
            Rigidbody destRb = destRigidbodies[i];

            // Rigidbody 속성 복사
            destRb.mass = srcRb.mass;
            destRb.drag = srcRb.drag;
            destRb.angularDrag = srcRb.angularDrag;
            destRb.useGravity = srcRb.useGravity;
            // 추가로 필요한 속성이 있으면 여기에 복사 로직 추가
        }
    }

    void CopyJoints()
    {
        Joint[] srcJoints = src.GetComponentsInChildren<Joint>();
        Joint[] destJoints = dest.GetComponentsInChildren<Joint>();

        for (int i = 0; i < srcJoints.Length; ++i)
        {
            Joint srcJoint = srcJoints[i];
            Joint destJoint = destJoints[i];

            // ConfigurableJoint 복사
            if (srcJoint is ConfigurableJoint srcConfigJoint && destJoint is ConfigurableJoint destConfigJoint)
            {
                destConfigJoint.massScale = srcConfigJoint.massScale;
                // 추가로 필요한 속성이 있으면 여기에 복사 로직 추가
            }
            // SpringJoint 복사
            else if (srcJoint is SpringJoint srcSpringJoint && destJoint is SpringJoint destSpringJoint)
            {
                destSpringJoint.massScale = srcSpringJoint.massScale;
                // 추가로 필요한 속성이 있으면 여기에 복사 로직 추가
            }
        }
    }

    void CopyColliders()
    {
        Collider[] srcColliders = src.GetComponentsInChildren<Collider>();
        Collider[] destColliders = dest.GetComponentsInChildren<Collider>();

        for (int i = 0; i < srcColliders.Length; ++i)
        {
            Collider srcCollider = srcColliders[i];
            Collider destCollider = destColliders[i];

            // Collider의 유형에 따라 복사 로직을 구현
            // 예: BoxCollider, SphereCollider, CapsuleCollider 등
            // 각 Collider 유형에 따라 필요한 속성을 복사합니다.
            // 예를 들어, BoxCollider의 경우:
            if (srcCollider is BoxCollider srcBoxCollider && destCollider is BoxCollider destBoxCollider)
            {
                destBoxCollider.center = srcBoxCollider.center;
                destBoxCollider.size = srcBoxCollider.size;
                // 추가로 필요한 속성이 있으면 여기에 복사 로직 추가
            }
        }
    }
}

