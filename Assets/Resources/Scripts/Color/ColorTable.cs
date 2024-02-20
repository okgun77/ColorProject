using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]


public class ColorTable : MonoBehaviour
{
    // 색상 정보를 저장하는 리스트
    [SerializeField] private List<ColorBasicInfo> basicColors; // 기본 색상 목록
    [SerializeField] private List<ColorBasicInfo> targetColors; // 목표 색상 목록
    [SerializeField] private List<ColorBasicInfo> mixColors; // 섞인 결과 색상 목록

    // 색상 번호로 기본 색상을 검색
    public ColorBasicInfo GetBasicColor(string _colorNo)
    {
        return basicColors.FirstOrDefault(color => color.ColorNo == _colorNo);
    }

    // 색상 번호로 혼합 색상을 검색
    public ColorBasicInfo GetMixColor(string _colorNo)
    {
        return mixColors.FirstOrDefault(color => color.ColorNo == _colorNo);
    }

    // 색상 번호로 목표 색상을 검색
    public ColorBasicInfo GetTargetColor(string _colorNo)
    {
        return targetColors.FirstOrDefault(color => color.ColorNo == _colorNo);
    }



    public ColorBasicInfo MixColors(string _colorNo1, string _colorNo2)
    {
        var colorNos = new List<string> { _colorNo1, _colorNo2 };
        colorNos.Sort();
        var combinedColorNo = string.Join("", colorNos);

        var mixedColor = GetMixColor(combinedColorNo);
        if (mixedColor != null)
        {
            return mixedColor;
        }
        
        return null;
    }


    // 외부에서 접근 가능한 public 메소드
    public ColorBasicInfo GetRandomTargetColor()
    {
        if (targetColors.Count == 0)
        {
            return null;
        }
        int randomIndex = Random.Range(0, targetColors.Count);
        return targetColors[randomIndex];
    }

    // 모든 목표 색상의 리스트를 반환하는 메소드
    public List<ColorBasicInfo> GetAllTargetColors()
    {
        return targetColors;
    }
    
}
