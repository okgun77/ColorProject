using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_MainMenu : MonoBehaviour
{
    public float speed = 5.0f; // 캐릭터 이동 속도

    private Rigidbody rb; // 캐릭터의 Rigidbody

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 가져오기
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // 좌우 이동 (A, D 키)
        float moveVertical = Input.GetAxis("Vertical"); // 전후 이동 (W, S 키)

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical); // 이동 벡터 생성

        rb.velocity = movement * speed; // Rigidbody의 속도를 설정하여 캐릭터 이동
    }
}
