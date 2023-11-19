using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyComponent : MonoBehaviour
{
    public GameObject sourceObject;
    public GameObject targetObject;

    void Start()
    {
        CopyComponents();
    }

    void CopyComponents()
    {
        if (sourceObject == null || targetObject == null)
        {
            Debug.LogError("Source or Target object is not set");
            return;
        }

        // Rigidbody 복사
        ObjectCopyComponent<Rigidbody>(sourceObject, targetObject, CopyRigidbody);

        // ConfigurableJoint 복사
        ObjectCopyComponent<ConfigurableJoint>(sourceObject, targetObject, CopyConfigurableJoint);

        // 콜라이더 복사
        CopyCollider(sourceObject, targetObject);

        // 레이어 값 복사
        targetObject.layer = sourceObject.layer;
    }

    void CopyCollider(GameObject source, GameObject target)
    {
        Collider sourceCollider = source.GetComponent<Collider>();

        if (sourceCollider is BoxCollider)
        {
            BoxCollider collider = target.AddComponent<BoxCollider>();
            BoxCollider sourceBoxCollider = sourceCollider as BoxCollider;
            collider.center = sourceBoxCollider.center;
            collider.size = sourceBoxCollider.size;
        }
        else if (sourceCollider is SphereCollider)
        {
            SphereCollider collider = target.AddComponent<SphereCollider>();
            SphereCollider sourceSphereCollider = sourceCollider as SphereCollider;
            collider.center = sourceSphereCollider.center;
            collider.radius = sourceSphereCollider.radius;
        }
        else if (sourceCollider is CapsuleCollider)
        {
            CapsuleCollider collider = target.AddComponent<CapsuleCollider>();
            CapsuleCollider sourceCapsuleCollider = sourceCollider as CapsuleCollider;
            collider.center = sourceCapsuleCollider.center;
            collider.radius = sourceCapsuleCollider.radius;
            collider.height = sourceCapsuleCollider.height;
            collider.direction = sourceCapsuleCollider.direction;
        }
        // 추가적인 콜라이더 타입에 대한 처리를 여기에 추가합니다.
    }

    void CopyRigidbody(Rigidbody source, Rigidbody target)
    {
        // Rigidbody 속성 복사
        target.mass = source.mass;
        target.drag = source.drag;
        target.angularDrag = source.angularDrag;
        target.useGravity = source.useGravity;
        target.isKinematic = source.isKinematic;
        // 추가적으로 필요한 필드들을 여기에 복사하세요.
    }

    void CopyConfigurableJoint(ConfigurableJoint source, ConfigurableJoint target)
    {
        // ConfigurableJoint 속성 복사
        target.connectedBody = source.connectedBody;
        // 추가적으로 필요한 필드들을 여기에 복사하세요.
    }

    void ObjectCopyComponent<T>(GameObject source, GameObject target, Action<T, T> copier) where T : Component
    {
        T sourceComponent = source.GetComponent<T>();
        if (sourceComponent != null)
        {
            T targetComponent = target.AddComponent<T>();
            copier(sourceComponent, targetComponent);
        }
    }
}
