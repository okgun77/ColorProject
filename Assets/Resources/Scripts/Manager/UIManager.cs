using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.PlayerLoop;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerColor playerColor; // �÷��̾� ���� ��ũ��Ʈ ����
    [SerializeField] private Image[] colorImages; // UI�� ǥ���� �̹��� �迭
    [SerializeField] private ColorTable colorTable; // ���� ���̺� ����
    [SerializeField] private GameObject ingameMainMenu;
    [SerializeField] private GameObject ingameOptionMenu;
    [SerializeField] private GameObject graphicOptionMenu;
    [SerializeField] private GameObject soundOptionMenu;
    [SerializeField] private GameObject keyboardOptionMenu;
    
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

    // �÷��̾��� ���� ����Ʈ�� UI�� ������Ʈ�ϴ� �޼ҵ�
    public void UpdateColorUI(List<string> _playerColors)
    {
        // ��� ���� �̹����� ����
        foreach (var image in colorImages)
        {
            image.gameObject.SetActive(false);
        }

        // �÷��̾� ���� ����Ʈ�� �ִ� �������� �̹����� ������Ʈ
        for (int i = 0; i < _playerColors.Count && i < colorImages.Length; ++i)
        // for (int i = 0; i < colorImages.Length; ++i)
        {
            ColorBasicInfo colorInfo = colorTable.GetBasicColor(_playerColors[i]);
            if (colorInfo != null)
            {
                // ���� ���� �������� ���� ���� 1�� ����
                colorImages[i].gameObject.SetActive(true);
                Color colorToApply = colorInfo.ColorValue;
                colorToApply.a = 1f; // ���� ���� 1�� ����
                colorImages[i].color = colorToApply;
                
            }
        }
    }
}