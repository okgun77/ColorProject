using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollisionEffect : MonoBehaviour
{
    [SerializeField] private float forceMultiplier = 2.0f; // 힘의 배수를 조절합니다.

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Detected"); // 디버그 로그를 추가하여 충돌 감지 확인

        // 회전체와의 충돌을 감지합니다.
        if (collision.gameObject.CompareTag("RotateObject")) // 태그 확인
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null && !rb.isKinematic) // Rigidbody가 kinematic이 아닌지 확인
            {
                // 충돌 지점에서 가장 가까운 점을 기준으로 반대 방향으로 힘을 가합니다.
                Vector3 forceDirection = transform.position - collision.contacts[0].point;
                forceDirection = forceDirection.normalized; // 방향만 필요하므로 정규화합니다.

                // 회전 속도와 충돌 지점의 방향을 기반으로 힘을 계산합니다.
                Vector3 force = forceDirection * collision.relativeVelocity.magnitude * forceMultiplier;

                // Rigidbody에 힘을 적용합니다.
                rb.AddForce(force, ForceMode.Impulse);
            }
            else
            {
                Debug.LogWarning("Rigidbody is kinematic or not attached to the object");
            }
        }
    }
}
