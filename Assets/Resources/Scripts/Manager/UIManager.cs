using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.PlayerLoop;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerColor playerColor; // 플레이어 색상 스크립트 참조
    [SerializeField] private Image[] colorImages; // UI에 표시할 이미지 배열
    [SerializeField] private ColorTable colorTable; // 색상 테이블 참조
    [SerializeField] private GameObject ingameMainMenu;
    [SerializeField] private GameObject ingameOptionMenu;
    [SerializeField] private GameObject graphicOptionMenu;
    [SerializeField] private GameObject soundOptionMenu;
    [SerializeField] private GameObject keyboardOptionMenu;
    
    [SerializeField] private Image targetColorDisplay;
    [SerializeField] private Image[] requiredColorDisplays;


    public static UIManager Instance { get; private set; }

    public void Init()
    {
        ingameMainMenu.SetActive(false);
        ingameOptionMenu.SetActive(false);
    }
    public void ShowPauseMenu()
    {
        ingameMainMenu.SetActive(true);
        ingameOptionMenu.SetActive(false);
    }

    public void HidePauseMenu()
    {
        ingameMainMenu.SetActive(false);
    }

    public void ShowOptionMenu()
    {
        ingameOptionMenu.SetActive(true);
        ingameMainMenu.SetActive(false);
    }

    public void BackToingameMainMenu()
    {
        ingameOptionMenu.SetActive(false);
        ingameMainMenu.SetActive(true);
    }


    // 게임 결과 표시 메소드 (성공, 실패 UI 업데이트)
    public void ShowGameResult(bool _isSuccess)
    {
        // 성공 또는 실패에 따라 UI 상태 업데이트
        // 예를 들어, 성공/실패 메시지를 보여주거나 관련 애니메이션을 재생
    }

    public void SetTargetColor(Color _color)
    {
        if (targetColorDisplay != null)
        {
            _color.a = 1f;
            targetColorDisplay.color = _color; // 타겟 색상 이미지의 색상을 설정
        }
        else
        {
            Debug.LogError("Target color display is not set in the inspector");
        }
    }


    public void DisplayRequiredColors(List<ColorBasicInfo> requiredColors)
    {
        for (int i = 0; i < requiredColorDisplays.Length; i++)
        {
            if (i < requiredColors.Count)
            {
                // Color 객체의 복사본을 만들어서 알파값을 1로 설정
                Color colorWithAlpha = requiredColors[i].ColorValue;
                colorWithAlpha.a = 1f; // 알파값을 1로 설정하여 불투명하게 만듦

                requiredColorDisplays[i].color = colorWithAlpha; // 색상 설정
                requiredColorDisplays[i].gameObject.SetActive(true); // 해당 이미지 활성화
            }
            else
            {
                requiredColorDisplays[i].gameObject.SetActive(false); // 없는 색상은 비활성화
            }
        }
    }



    // 플레이어의 색상 리스트를 UI에 업데이트하는 메소드
    public void UpdateColorUI(List<string> _playerColors)
    {
        // 모든 색상 이미지를 숨김
        /*
        foreach (var image in colorImages)
        {
            image.gameObject.SetActive(false);
        }
        */

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