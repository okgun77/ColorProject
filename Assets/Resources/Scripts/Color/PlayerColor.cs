using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerColor : MonoBehaviour
{
    [SerializeField] private List<Color> colorList = new List<Color>(); // 색상 기록 리스트
    [SerializeField] private UIManager uiManager;
    public Color defaultColor = Color.gray; // 초기 색상
    private Renderer[] playerRenderers; // 플레이어의 모든 렌더러 컴포넌트 저장 배열

    void Start()
    {
        // 플레이어 프리팹의 자식 렌더러들 GET.
        playerRenderers = GetComponentsInChildren<Renderer>();

        if (playerRenderers.Length == 0)
        {
            Debug.LogError("No Renderer components found on " + gameObject.name);
            return; // 렌더러가 없으면 return
        }

        //색상 초기화.
        SetColor(defaultColor);
    }

    public void AddColor(Color _newColor)
    {
        // 색상 리스트가 이미 5개이고, 새로운 색상 추가 시 검은색으로 바꿈.
        if (colorList.Count >= 4)
        {
            colorList.Clear();
            colorList.Add(Color.black);
            SetColor(Color.black);
            return;
        }
    
        // 추가하려는 색상이 리스트에 이미 있는지, 또는 첫 색상이 검은색인지 확인.
        if (colorList.Contains(_newColor) || (colorList.Count > 0 && colorList[0] == Color.black))
        {
            return;
        }

        // 색상 리스트에 색상 추가
        colorList.Add(_newColor);
        uiManager.UpdateColorUI(colorList); // UI 업데이트 호출

        // 혼합된 색상으로 플레이어 색상을 설정
        SetColor(MixColor());
        uiManager.UpdateColorUI(colorList);
    }



    public void ResetColor()
    {
        colorList.Clear(); // 색상 리스트 Clear.
        SetColor(defaultColor); // 초기 색상으로 설정.
        uiManager.UpdateColorUI(colorList); // UI 업데이트 호출
        Debug.Log("ResetColor called and UI should be updated.");
    }

    private Color MixColor()
    {
        // 리스트에 색상이 없으면 기본 색상을 반환.
        if (colorList.Count == 0) return defaultColor;
        
        // 리스트의 모든 색상을 섞어서 새로운 색상을 계산.
        Color mixedColor = colorList.Aggregate(Color.clear, (current, color) => current + color);
        mixedColor /= colorList.Count; // 색상평균
        mixedColor.a = 1; // 알파값을 1로 설정.
        return mixedColor;
    }

    private void SetColor(Color _color)
    {
        // 모든 렌더러 컴포넌트에 새 색상을 설정합니다.
        foreach (var renderer in playerRenderers)
        {
            renderer.material.color = _color;
        }
        
        Debug.Log("Current Player Color: " + _color);
    }
}
