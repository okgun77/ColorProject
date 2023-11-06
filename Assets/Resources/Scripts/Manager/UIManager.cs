using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public List<Image> colorImages; // 색상을 표시할 Image 리스트

    // 플레이어의 컬러 리스트를 UI에 업데이트
    public void UpdateColorUI(List<Color> _playerColors)
    {
        // 각각의 Image 컴포넌트에 대해 실행.
        for (int i = 0; i < colorImages.Count; i++)
        {
            if (i < _playerColors.Count)
            {
                // Image 활성화하고 색상을 설정.
                
                // colorImages[i].color = _playerColors[i];
                // colorImages[i].gameObject.SetActive(true);
                colorImages[i].gameObject.SetActive(true);
                Color colorWithFullAlpha = _playerColors[i];
                colorWithFullAlpha.a = 1f; // 알파값을 1로 설정.
                colorImages[i].color = colorWithFullAlpha;
                Debug.Log($"Image {i} {_playerColors[i]} 활성화");
            }
            else
            {
                // 나머지 Image는 비활성화.
                // colorImages[i].gameObject.SetActive(false);
                Debug.Log($"Image {i} 비활성화.");
            }
        }
    }

    
    public void ResetColorUI()
    {
        foreach (var image in colorImages)
        {
            image.gameObject.SetActive(false);
        }
    }
}