using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public List<Image> colorImages; // ������ ǥ���� Image ����Ʈ

    // �÷��̾��� �÷� ����Ʈ�� UI�� ������Ʈ
    public void UpdateColorUI(List<Color> _playerColors)
    {
        // ������ Image ������Ʈ�� ���� ����.
        for (int i = 0; i < colorImages.Count; i++)
        {
            if (i < _playerColors.Count)
            {
                // Image Ȱ��ȭ�ϰ� ������ ����.
                
                // colorImages[i].color = _playerColors[i];
                // colorImages[i].gameObject.SetActive(true);
                colorImages[i].gameObject.SetActive(true);
                Color colorWithFullAlpha = _playerColors[i];
                colorWithFullAlpha.a = 1f; // ���İ��� 1�� ����.
                colorImages[i].color = colorWithFullAlpha;
                Debug.Log($"Image {i} {_playerColors[i]} Ȱ��ȭ");
            }
            else
            {
                // ������ Image�� ��Ȱ��ȭ.
                // colorImages[i].gameObject.SetActive(false);
                Debug.Log($"Image {i} ��Ȱ��ȭ.");
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