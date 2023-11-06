using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class ColorTable : MonoBehaviour
{
    // 색상 정보를 저장하는 리스트들을 정의합니다.
    [SerializeField]
    private List<ColorBasicInfo> basicColors; // 기본 색상 목록
    [SerializeField]
    private List<ColorBasicInfo> targetColors; // 목표 색상 목록
    [SerializeField]
    private List<ColorBasicInfo> mixColors; // 섞인 결과 색상 목록

    // 색상을 추가하는 메서드들
    public void AddBasicColor(ColorBasicInfo _colorInfo)
    {
        if (!basicColors.Any(c => c.ColorName == _colorInfo.ColorName))
        {
            basicColors.Add(_colorInfo);
        }
    }

    public void AddTargetColor(ColorBasicInfo _colorInfo)
    {
        if (!targetColors.Any(c => c.ColorName == _colorInfo.ColorName))
        {
            targetColors.Add(_colorInfo);
        }
    }

    public void AddMixColor(ColorBasicInfo _colorInfo)
    {
        if (!mixColors.Any(c => c.ColorName == _colorInfo.ColorName))
        {
            mixColors.Add(_colorInfo);
        }
    }

    // 색상을 이름으로 검색하는 메서드
    public ColorBasicInfo GetColorByName(string _name)
    {
        return basicColors.Concat(targetColors).Concat(mixColors).FirstOrDefault(colorInfo => colorInfo.ColorName == _name);
    }

    // 색상을 RGB 값으로 검색하는 메서드
    public ColorBasicInfo GetColorByValue(Color _colorValue)
    {
        return basicColors.Concat(targetColors).Concat(mixColors).FirstOrDefault(colorInfo => colorInfo.ColorValue == _colorValue);
    }

    // 두 색상을 입력으로 받아 섞어서 새로운 색상을 만드는 메서드
    // 이 메서드는 두 색상을 섞는 실제 로직을 구현해야 합니다.
    public Color MixTwoColors(Color color1, Color color2)
    {
        // 이 부분은 색상을 섞는 실제 알고리즘으로 구현되어야 합니다.
        // 예제는 단순히 평균을 사용합니다.
        Color mixResult = (color1 + color2) / 2;
        mixResult.a = 1; // 알파 값을 최대로 설정하여 불투명하게 만듭니다.
        return mixResult;
    }

    // 기본 색상 리스트를 반환하는 메서드
    public List<ColorBasicInfo> GetBasicColors()
    {
        return basicColors;
    }

    // 목표 색상 리스트를 반환하는 메서드
    public List<ColorBasicInfo> GetTargetColors()
    {
        return targetColors;
    }

    // 섞인 색상 리스트를 반환하는 메서드
    public List<ColorBasicInfo> GetMixColors()
    {
        return mixColors;
    }

    // 기타 필요한 메서드들...
}
