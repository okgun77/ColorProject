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
        Debug.Log("Triggered with: " + _other.name);

        if (_other.CompareTag("Player"))
        {
            PlayerColor playerColor = _other.GetComponent<PlayerColor>();
            if (playerColor == null)
            {
                Debug.LogError("PlayerColor component not found on " + _other.name);
                return; // 여기서 함수 실행을 중단합니다.
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