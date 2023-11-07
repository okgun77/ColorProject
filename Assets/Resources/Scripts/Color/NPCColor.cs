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
    private void OnTriggerEnter(Collider other)
    {
        // 플레이어와의 충돌 확인
        if (other.CompareTag("Player"))
        {
            PlayerColor playerColor = other.GetComponent<PlayerColor>();

            // NPC 유형에 따른 조치
            switch (type)
            {
                case NPCType.NPC_COLOR:
                    // 플레이어에게 색상 추가
                    playerColor.GetColor(colorNo);
                    break;
                case NPCType.NPC_WATER:
                    // 플레이어 색상 초기화
                    playerColor.ResetColor();
                    break;
            }
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