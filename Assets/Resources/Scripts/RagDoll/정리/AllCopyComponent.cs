using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCopyComponent : MonoBehaviour
{
    [SerializeField] private GameObject src = null;     // 원본
    [SerializeField] private GameObject dest = null;    // 복사원하는 곳

    // Start is called before the first frame update
    void Start()
    {
        if (src == null || dest == null) return;

        // src와 dest의 하위 오브젝트들을 가져옵니다.
        Transform[] srcChildren = src.GetComponentsInChildren<Transform>();
        Transform[] destChildren = dest.GetComponentsInChildren<Transform>();

        // src와 dest의 하위 오브젝트 수가 같아야 합니다.
        if (srcChildren.Length != destChildren.Length)
        {
            Debug.LogError("src와 dest의 하위 오브젝트 수가 다릅니다.");
            return;
        }

        // src와 dest의 하위 오브젝트를 하나씩 비교하며 컴포넌트를 복사합니다.
        for (int i = 0; i < srcChildren.Length; i++)
        {
            CopyAllComponentsFromSourceToDestination(srcChildren[i], destChildren[i]);
        }
    }

    // 컴포넌트를 모두 복사하는 함수
    private void CopyAllComponentsFromSourceToDestination(Transform srcTransform, Transform destTransform)
    {
        if (srcTransform == null || destTransform == null) return;

        // src 오브젝트의 모든 컴포넌트 배열을 가져옵니다.
        Component[] srcComponents = srcTransform.GetComponents<Component>();

        foreach (var srcComponent in srcComponents)
        {
            if (srcComponent == null) continue;

            // 컴포넌트 타입을 가져옵니다.
            System.Type componentType = srcComponent.GetType();

            // dest 오브젝트에 해당 타입의 컴포넌트가 이미 존재하면 스킵합니다.
            if (destTransform.GetComponent(componentType) != null) continue;

            // 컴포넌트를 dest 오브젝트에 복사합니다.
            Component destComponent = destTransform.gameObject.AddComponent(componentType);

            // Rigidbody 컴포넌트인 경우, 질량을 복사합니다.
            if (srcComponent is Rigidbody && destComponent is Rigidbody)
            {
                Rigidbody srcRigidbody = (Rigidbody)srcComponent;
                Rigidbody destRigidbody = (Rigidbody)destComponent;
                destRigidbody.mass = srcRigidbody.mass;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
