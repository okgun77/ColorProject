using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ColorManager : MonoBehaviour
{
    [SerializeField] private List<ColorInfo> basicColors; // 기본 색상 목록
    [SerializeField] private List<ColorInfo> targetColors; // 목표 색상 목록
    [SerializeField] private List<ColorInfo> otherColors; // 나머지 색상 목록

    // 플레이어의 현재 색상 스택
    private Stack<Color> colorStack = new Stack<Color>();

    void Start()
    {
        // 초기화 코드는 필요에 따라 여기에 작성
    }

    // 색상을 스택에 추가하는 메서드
    public void AddColor(Color _color)
    {
        colorStack.Push(_color);
        StackCheck();
    }

    // 스택 평가 및 캐릭터 색상 업데이트 메서드
    private void StackCheck()
    {
        if (colorStack.Count > 5)
        {
            SetColor(Color.black); // 스택이 5개를 초과하면 색상을 검정색으로 설정
        }
        else
        {
            Color mixedColor = MixColors(colorStack); // 스택의 색상을 섞음
            SetColor(mixedColor); // 섞인 색상으로 캐릭터 색상을 설정
        }
    }

    // 색상을 섞는 메서드
    private Color MixColors(Stack<Color> _colors)
    {
        // 색상을 섞는 로직 구현
        // 예제 코드는 단순하게 평균값을 사용합니다.
        Color result = new Color(0, 0, 0, 0); // 알파 채널도 고려할 경우
        foreach (Color c in _colors)
        {
            result += c;
        }
        result /= _colors.Count; // 색상을 총 개수로 나누어 평균을 구함
        result.a = 1; // 알파 값을 설정 (투명도 없음)

        return result;
    }

    // 캐릭터 색상을 설정하는 메서드
    private void SetColor(Color _color)
    {
        // 캐릭터의 색상을 설정하는 로직을 여기에 구현
        // 예: renderer.material.color = _color;
    }

    // 색상 스택을 비우는 메서드
    public void ResetColors()
    {
        colorStack.Clear();
        SetColor(GetBaseColor()); // 기본 색상으로 리셋
    }

    // 기본 색상을 가져오는 메서드
    private Color GetBaseColor()
    {
        // 기본 색상을 반환 (예를 들어, 회색 또는 흰색)
        // 이 예제에서는 단순히 하드코딩된 색상을 반환합니다.
        return Color.gray;
    }

    // 현재 색상이 목표 색상 중 하나인지 확인하는 메서드
    public bool IsTargetColor(Color _color)
    {
        // 목표 색상 목록에서 색상을 검색하고, 있으면 true를 반환합니다.
        return targetColors.Any(colorInfo => colorInfo.Color.Equals(_color));
    }

    // ... 추가적인 색상 관련 로직 및 유틸리티 메서드들
}
