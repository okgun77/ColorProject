using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider _other)
    {
        Debug.Log("충돌검사 테스트!!");
    }
}
