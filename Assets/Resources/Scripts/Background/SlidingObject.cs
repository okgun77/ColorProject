using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SlidingObject : MonoBehaviour
{
    [SerializeField] private Rigidbody rb; // 오브젝트의 Rigidbody 참조
    [SerializeField] private Transform shipTransform; // 배의 Transform 참조

    [SerializeField] private float slidingCoefficient = 0.5f; // 미끄러짐 계수

    void FixedUpdate()
    {
        // 배의 Transform이 유효한지 확인
        if (shipTransform)
        {
            // 배의 기울기를 가져와서 미끄러짐 힘을 계산합니다.
            Vector3 shipUp = shipTransform.up;
            Vector3 gravity = Physics.gravity;
            Vector3 slidingForce = Vector3.ProjectOnPlane(gravity, shipUp) * slidingCoefficient;

            // 계산된 미끄러짐 힘을 적용합니다.
            rb.AddForce(slidingForce, ForceMode.Acceleration);
        }
    }
}