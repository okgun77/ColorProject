using TMPro; // TextMeshPro 네임스페이스 추가
using UnityEngine;

public class NPCStateDisplay : MonoBehaviour
{
    public TextMeshProUGUI stateText; // Inspector에서 할당

    private void Update()
    {
        UpdateStateDisplay("State"); // 현재 상태에 따라 문자열을 업데이트
    }

    public void UpdateStateDisplay(string _state)
    {
        if (stateText != null)
        {
            stateText.text = _state;
        }
    }
}

