using UnityEngine;

public class NPCColor_Backup : MonoBehaviour
{
    public enum NPCType { NPC_COLOR, NPC_WATER } // NPC의 유형을 정의합니다.

    public NPCType npcType; // NPC의 유형.
    public Color colorNPCToPlayer; // NPC COLOR 색상.
    private Renderer npcRenderer; // NPC 렌더러

    void Start()
    {
        npcRenderer = GetComponentInChildren<Renderer>();
        if (npcRenderer == null)
        {
            Debug.LogError("NPC Renderer not found on " + gameObject.name);
        }
        else
        {
            // NPC의 색상을 설정.
            npcRenderer.material.color = colorNPCToPlayer;
        }
    }
    
    private void OnTriggerEnter(Collider _other)
    {
        
        Debug.Log("OnTriggerEnter called with: " + _other.gameObject.name); // 호출 로그
        
        // 'Player' 태그를 가진 오브젝트와의 충돌을 확인.
        if (_other.CompareTag("Player"))
        {
            Debug.Log("Player tagged object entered the trigger"); // 플레이어 태그 확인 로그
            PlayerColor playerColor = _other.GetComponent<PlayerColor>();

            if (playerColor != null)
            {
                Debug.Log("PlayerColor component found. Adding color: " + colorNPCToPlayer); // 색상 추가 확인 로그 
                switch (npcType)
                {
                    case NPCType.NPC_COLOR:
                        playerColor.AddColor(colorNPCToPlayer);
                        break;
                    case NPCType.NPC_WATER:
                        playerColor.ResetColor();
                        break;
                }
            }
            else
            {
                Debug.LogError("PlayerColor component not found in " + _other.gameObject.name);
            }
        }
    }


}