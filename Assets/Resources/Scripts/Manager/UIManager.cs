using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerColor playerColor; // 플레이어 색상 스크립트 참조
    [SerializeField] private Image[] colorImages; // UI에 표시할 이미지 배열
    [SerializeField] private ColorTable colorTable; // 색상 테이블 참조

    void Start()
    {
        // UpdateColorUI(); // 게임 시작 시 UI 업데이트
    }

    void Update()
    {
        // 여기서는 플레이어의 색상 리스트에 변동이 있는지를 체크하지 않고 있습니다.
        // 실제 게임에서는 플레이어의 색상 리스트 변동을 감지하는 다른 메커니즘이 필요할 수 있습니다.
    }

    // 플레이어의 색상 리스트를 UI에 업데이트하는 메소드
    public void UpdateColorUI(List<string> _playerColors)
    {
        // 모든 색상 이미지를 숨김
        foreach (var image in colorImages)
        {
            image.gameObject.SetActive(false);
        }

        // 플레이어 색상 리스트에 있는 색상으로 이미지를 업데이트
        for (int i = 0; i < _playerColors.Count && i < colorImages.Length; ++i)
        // for (int i = 0; i < colorImages.Length; ++i)
        {
            ColorBasicInfo colorInfo = colorTable.GetBasicColor(_playerColors[i]);
            if (colorInfo != null)
            {
                // 색상 값을 가져오고 알파 값을 1로 설정
                colorImages[i].gameObject.SetActive(true);
                Color colorToApply = colorInfo.ColorValue;
                colorToApply.a = 1f; // 알파 값을 1로 설정
                colorImages[i].color = colorToApply;
                
            }
        }
    }
}