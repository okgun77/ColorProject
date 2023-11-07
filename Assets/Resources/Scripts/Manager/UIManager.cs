using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerColor playerColor; // �÷��̾� ���� ��ũ��Ʈ ����
    [SerializeField] private Image[] colorImages; // UI�� ǥ���� �̹��� �迭
    [SerializeField] private ColorTable colorTable; // ���� ���̺� ����

    void Start()
    {
        // UpdateColorUI(); // ���� ���� �� UI ������Ʈ
    }

    void Update()
    {
        // ���⼭�� �÷��̾��� ���� ����Ʈ�� ������ �ִ����� üũ���� �ʰ� �ֽ��ϴ�.
        // ���� ���ӿ����� �÷��̾��� ���� ����Ʈ ������ �����ϴ� �ٸ� ��Ŀ������ �ʿ��� �� �ֽ��ϴ�.
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