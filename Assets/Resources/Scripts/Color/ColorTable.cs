using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class ColorTable : MonoBehaviour
{
    // 색상 정보를 저장하는 리스트
    [SerializeField]
    private List<ColorBasicInfo> basicColors; // 기본 색상 목록
    [SerializeField]
    private List<ColorBasicInfo> targetColors; // 목표 색상 목록
    [SerializeField]
    private List<ColorBasicInfo> mixColors; // 섞인 결과 색상 목록

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
        // 두 색상 번호를 정렬하여 새로운 혼합 색상 번호 생성
        var colorNos = new List<string> { _colorNo1, _colorNo2 };
        colorNos.Sort();
        var combinedColorNo = string.Join("", colorNos);

        // 혼합 색상 목록에서 해당 혼합 색상 번호를 가진 색상 정보를 찾음
        var mixedColor = GetMixColor(combinedColorNo);
        if (mixedColor != null) // 혼합 색상이 목록에 있으면 반환
        {
            return mixedColor;
        }
        
        // 혼합 색상이 없을 경우, 기본 색상을 찾아 혼합을 시도할 수 있는 로직을 추가할 수 있음.
        // 그러나 이 예시에서는 미리 정의된 혼합 색상만 사용.

        return null; // 혼합 색상을 찾을 수 없는 경우
    }
    
    // 외부에서 접근 가능한 public 메소드
    public ColorBasicInfo GetRandomTargetColor()
    {
        if (targetColors.Count == 0)
        {
            Debug.LogError("Target colors list is empty.");
            return null; // 또는 기본 ColorBasicInfo 반환
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
