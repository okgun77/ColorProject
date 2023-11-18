using UnityEngine;

public enum NPCType { NPC_COLOR, NPC_WATER } // NPC의 유형 정의

public class NPCColor : MonoBehaviour
{
    [SerializeField] private Renderer npcRenderer; // NPC의 Renderer 컴포넌트
    [SerializeField] private string colorNo; // NPC가 가지고 있는 색상 번호
    [SerializeField] private NPCType type; // NPC의 유형
    [SerializeField] private ColorTable colorTable; // 색상 테이블 참조
    
    // 프로퍼티
    public string ColorNo => colorNo;
    public NPCType Type => type;

    
    
    // NPC의 기본 색상 설정
    private void Start()
    {
        ApplyColor();
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