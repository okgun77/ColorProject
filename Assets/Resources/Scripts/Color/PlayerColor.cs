using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerColor : MonoBehaviour
{
    [SerializeField] private Renderer[] playerRenderers; // 플레이어의 Renderer 컴포넌트 배열
    [SerializeField] private ColorTable colorTable; // 색상 테이블
    [SerializeField] private UIManager uiManager; // UIManager
    
    private List<string> getColors; // 플레이어가 습득한 색상 번호 목록
    private bool isColorLocked; // 색상 고정

    void Start()
    {
        getColors = new List<string>();
        // 초기 색상 설정을 위한 호출
        UpdatePlayerColor();
    }

    // 색상 습득 함수
    public void GetColor(string _colorNo)
    {
        // 색상이 검은색으로 고정되었다면 새로운 색상 추가를 하지 않음
        if (isColorLocked || getColors.Contains(_colorNo)) // 이미 가진 색상인지 확인
            return;

        getColors.Add(_colorNo);
        if (getColors.Count >= 5) // 색상 수가 5개 이상이면 검은색으로 설정
        {
            LockColorToBlack();
        }
        else
        {
            UpdatePlayerColor();
        }
    }
    
    // 플레이어가 현재 가진 색상 번호 리스트를 반환하는 메소드
    public List<string> GetCurrentColorList()
    {
        return new List<string>(getColors); // 현재 getColors 리스트의 복사본을 반환
    }
    
    // 주어진 색상 번호에 해당하는 Color 값을 반환하는 메소드
    public Color GetColorValue(string _colorNo)
    {
        // 색상 테이블에서 색상을 찾아 반환
        ColorBasicInfo colorInfo = colorTable.GetBasicColor(_colorNo);
        if (colorInfo != null)
        {
            return colorInfo.ColorValue; // 색상 값 반환
        }
        else
        {
            // 색상 정보가 없는 경우 기본 색상 반환
            Debug.LogWarning($"Color No not found in color table: {_colorNo}");
            return Color.white; // 색상 정보가 없으면 흰색 반환
        }
    }
    
    // 플레이어 색상을 검은색으로 고정
    private void LockColorToBlack()
    {
        isColorLocked = true;
        SetColor(Color.black);
    }
    
    // 지정된 색상으로 플레이어의 모든 Renderer를 업데이트
    
    private void SetColor(Color _color)
    {
        foreach (var renderer in playerRenderers)
        {
            // renderer.material.color = _color;
            
            if(renderer is SkinnedMeshRenderer)
            {
                (renderer as SkinnedMeshRenderer).material.color = _color;
            }
            else if(renderer is MeshRenderer)
            {
                (renderer as MeshRenderer).material.color = _color;
            }
        }
    }
    
    
    /*
    private void SetColor(Color _color)
    {
        float alpha = 0.05f; // 예시로 설정한 임의의 알파값 (0.0 완전 투명 ~ 1.0 완전 불투명)

        foreach (var renderer in playerRenderers)
        {
            // 주어진 색상에 임의의 알파값을 적용
            Color colorWithAlpha = new Color(_color.r, _color.g, _color.b, alpha);

            if(renderer is SkinnedMeshRenderer skinnedMeshRenderer)
            {
                skinnedMeshRenderer.material.color = colorWithAlpha;
            }
            else if(renderer is MeshRenderer meshRenderer)
            {
                meshRenderer.material.color = colorWithAlpha;
            }
        }
    }
    */

    
    // URP
    private void SetColor_URP(Color _color)
    {
        foreach (var renderer in playerRenderers)
        {
            if(renderer is SkinnedMeshRenderer skinnedMeshRenderer)
            {
                var material = skinnedMeshRenderer.material;
                material.SetColor("_BaseColor", _color); // 텍스처와 혼합되는 색상 변경
            }
            else if(renderer is MeshRenderer meshRenderer)
            {
                var material = meshRenderer.material;
                material.SetColor("_BaseColor", _color); // 텍스처와 혼합되는 색상 변경
            }
        }
    }
    

    // 플레이어 색상 업데이트 함수
    private void UpdatePlayerColor()
    {
        if (isColorLocked) return; // 색상이 고정되었다면 업데이트하지 않음
        
        // 기본 색상으로 초기화
        Color colorToApply = Color.white;

        // 습득한 색상이 하나뿐인 경우 해당 색상을 적용
        if (getColors.Count == 1)
        {
            colorToApply = colorTable.GetBasicColor(getColors[0]).ColorValue;
        }
        else if (getColors.Count > 1)
        {
            // 혼합 색상 계산
            string mixedColorNo = getColors[0]; // 초기 혼합 색상 번호를 첫 번째 습득한 색상으로 설정

            // 혼합 색상을 반복하여 계산
            for (int i = 1; i < getColors.Count; i++)
            {
                ColorBasicInfo mixedColorInfo = colorTable.MixColors(mixedColorNo, getColors[i]);
                if (mixedColorInfo != null)
                {
                    // 성공적으로 혼합된 색상을 적용
                    mixedColorNo = mixedColorInfo.ColorNo;
                    colorToApply = mixedColorInfo.ColorValue;
                }
                else
                {
                    // 혼합 색상을 찾을 수 없는 경우, 반복 중단
                    break;
                }
            }
        }
        // 습득한 색상이 없는 경우 기본 색상을 유지

        // 모든 Renderer에 색상 적용
        foreach (var renderer in playerRenderers)
        {
            renderer.material.color = colorToApply;
        }
        
        // 적용할 색상 설정 후 Renderer에 적용
        SetColor(colorToApply);
     
        // 색상 업데이트 후 UIManager의 UI 업데이트 메소드 호출
        uiManager.UpdateColorUI(getColors);
        
    }

    // 플레이어의 색상을 초기화하는 함수
    public void ResetColor()
    {
        isColorLocked = false; // 색상 고정 해제
        getColors.Clear();
        UpdatePlayerColor();
        uiManager.UpdateColorUI(getColors);
    }

    public bool IsTargetColorAchieved(ColorBasicInfo targetColorInfo)
    {
        // 습득한 색상이 없거나 리스트가 null인 경우, 목표 색상이 달성되지 않음을 반환
        if (getColors == null || getColors.Count == 0)
        {
            return false;
        }

        // 습득한 색상을 정렬하여 문자열로 합치기
        string playerCombinedColorNo = string.Join("", getColors.OrderBy(c => c));
        // 목표 색상 번호 가져오기
        string targetColorNo = targetColorInfo.ColorNo;

        // 플레이어의 조합된 색상과 목표 색상을 비교
        return playerCombinedColorNo.Equals(targetColorNo);
    }


    
}
