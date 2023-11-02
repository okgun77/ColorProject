using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCColor : MonoBehaviour
{
    [SerializeField] private Color npcColor;  // NPC�� ����

    private Renderer rend;

    public void Init()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = npcColor; // �ʱ� ���� ����
    }

    public Color StealColor()
    {
        Color stolenColor = npcColor;
        // ������ ���ܰ����Ƿ� NPC�� ������ �����ϰų� ��Ȱ��ȭ �� �� ����.
        // rend.material.color = Color.gray;    // ȸ������ ����
        // gameObject.SetActive(false);    // NPC�� ��Ȱ��ȭ

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
