using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCColor : MonoBehaviour
{
    [SerializeField] private Color npcColor;  // NPC의 색상

    private Renderer rend;

    public void Init()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = npcColor; // 초기 색상 설정
    }

    public Color StealColor()
    {
        Color stolenColor = npcColor;
        // 색상이 뺏겨갔으므로 NPC의 색상을 변경하거나 비활성화 할 수 있음.
        // rend.material.color = Color.gray;    // 회색으로 변경
        // gameObject.SetActive(false);    // NPC를 비활성화

        return stolenColor;
    }

    void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.CompareTag("Player"))
        {
            Color stolenColor = StealColor();
            _collision.gameObject.GetComponent<PlayerColor>().MixPlayerColor(stolenColor);
        }
    }
}
