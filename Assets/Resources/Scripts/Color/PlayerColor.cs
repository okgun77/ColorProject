using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerColor : MonoBehaviour
{
    [SerializeField] private Renderer[] playerRenderers; // 플레이어의 Renderer 컴포넌트 배열
    [SerializeField] private ColorTable colorTable; // 색상 테이블
    [SerializeField] private UIManager uiManager; // UIManager

    [SerializeField] private PlayerEffectController playerEffect; 

    private List<string> playerColors = new List<string>(); // 플레이어가 습득한 색상 번호 목록
    private bool isColorLocked; // 색상 고정

    private NPCColor npcColor;

    public void Init()
    {
        playerColors = new List<string>();
        // 초기 색상 설정을 위한 호출
        UpdatePlayerColor();
    }

    
    // 색상 습득 함수
    public void GetColor(string _colorNo)
    {
        // 색상이 검은색으로 고정되었다면 새로운 색상 추가를 하지 않음
        if (isColorLocked || playerColors.Contains(_colorNo)) // 이미 가진 색상인지 확인
        {
            return;
        }

        playerColors.Add(_colorNo);
        
        if (playerColors.Count >= 5) // 색상 수가 5개 이상이면 검은색으로 설정
        {
            LockColorToBlack();
        }
        else
        {
            UpdatePlayerColor();
        }
        
        GameManager.Instance.ColorMatchCheck();
    }
    
    // 플레이어가 현재 가진 색상 번호 리스트를 반환하는 메소드
    public List<string> GetCurrentColorList()
    {
        var currentColorList = new List<string>(playerColors);
        return currentColorList;        
    }

    // 플레이어 색상 업데이트 함수
    private void UpdatePlayerColor()
    {
        if (isColorLocked) return;          // 색상이 고정되었다면 업데이트하지 않음

        Color colorToApply = Color.white;   // 기본 색상 White

        if (playerColors.Count == 1)        // 습득한 색상이 하나뿐인 경우 해당 색상을 적용
        {
            colorToApply = colorTable.GetBasicColor(playerColors[0]).ColorValue;
        }
        else if (playerColors.Count > 1)
        {
            // 혼합 색상 계산
            string mixedColorNo = playerColors[0];          // 초기 혼합 색상 번호를 첫 번째 습득한 색상으로 설정

            
            for (int i = 1; i < playerColors.Count; i++)    // 혼합 색상을 반복하여 계산
            {
                ColorBasicInfo mixedColorInfo = colorTable.MixColors(mixedColorNo, playerColors[i]);
                if (mixedColorInfo != null)
                {
                    // 성공적으로 혼합된 색상을 적용
                    mixedColorNo = mixedColorInfo.ColorNo;
                    colorToApply = mixedColorInfo.ColorValue;
                }
                else
                {
                    break;
                }
            }
        }
        // 모든 Renderer에 색상 적용
        foreach (var renderer in playerRenderers)
        {
            renderer.material.color = colorToApply;
        }
        // 적용할 색상 설정 후 Renderer에 적용
        SetColor(colorToApply);
        // 색상 업데이트 후 UIManager의 UI 업데이트 메소드 호출
        uiManager.UpdateColorUI(playerColors);
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
    

    // 플레이어의 색상을 초기화하는 함수
    public void ResetColor()
    {
        isColorLocked = false; // 색상 고정 해제
        playerColors.Clear();
        UpdatePlayerColor();
        uiManager.UpdateColorUI(playerColors);   
    }

    
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("NPC"))
        {
            npcColor = _other.GetComponent<NPCColor>();
            if (npcColor == null)
            {
                return;
            }
            switch (npcColor.Type)
            {
                case NPCType.NPC_COLOR:
                    GetColor(npcColor.ColorNo);
                    effectPlay();
                    SoundManager.Instance.PlaySE(SoundManager.Instance.earncolorSound);
                    break;
                case NPCType.NPC_WATER:
                    effectPlay();
                    SoundManager.Instance.PlaySE(SoundManager.Instance.earncolorSound);

                    ResetColor();
                    break;
            }
        }
    }

    void effectPlay()
    {
        // 파티클 시스템의 인덱스를 지정하여 정지 (여기서는 0번째 파티클)
        playerEffect.PlayParticle(0);
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
            return Color.white; // 색상 정보가 없으면 흰색 반환
        }
    }
    
    public void PlayerColorCombineInfo(ColorBasicInfo _targetColor)
    {
        List<string> playerColorCodes = GetCurrentColorList();
        string playerCombinedColorNo = string.Join("", playerColorCodes.OrderBy(c => c));
        string targetColorNo = _targetColor.ColorNo;
    }

    
    // URP Test
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

}
