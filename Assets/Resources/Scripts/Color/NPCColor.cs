using UnityEngine;

public enum NPCType { NPC_COLOR, NPC_WATER } // NPC의 유형 정의

public class NPCColor : MonoBehaviour
{
    [SerializeField] private Renderer npcRenderer; // NPC의 Renderer 컴포넌트
    [SerializeField] private string colorNo; // NPC가 가지고 있는 색상 번호
    [SerializeField] private NPCType type; // NPC의 유형
    [SerializeField] private ColorTable colorTable; // 색상 테이블 참조
    

    // NPC의 기본 색상 설정
    private void Start()
    {
        ApplyColor();
    }

    // NPC와 플레이어가 닿을 때의 상호작용
    private void OnTriggerEnter(Collider _other)
    {
        if (!_other.CompareTag("Player"))
        {
            Debug.Log("PlayerTag아님 : " + _other.name);
            return;
        }

        Debug.Log("콜라이더 감지 : " + _other.name);
        PlayerColor playerColor = _other.GetComponent<PlayerColor>();

        if (playerColor == null)
        {
            Debug.Log("PlayerColor 없음 : " + _other.name);
            return;
        }
    
        // NPC 유형에 따른 조치
        switch (type)
        {
            case NPCType.NPC_COLOR:
                playerColor.GetColor(colorNo);
                break;
            case NPCType.NPC_WATER:
                playerColor.ResetColor();
                break;
        }
    }



    // NPC의 색상 적용
    private void ApplyColor()
    {
        ColorBasicInfo colorInfo = colorTable.GetBasicColor(colorNo);
        if (colorInfo != null)
        {
            npcRenderer.material.color = colorInfo.ColorValue;
        }
        else
        {
            Debug.LogWarning("Color No not found in color table: " + colorNo);
        }
    }
}