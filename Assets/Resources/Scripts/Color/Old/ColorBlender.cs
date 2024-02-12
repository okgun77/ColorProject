using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlender : MonoBehaviour
{
    
    [SerializeField] private ColorTable colorTable;
    
    // 색상 혼합
    Color BlendColors(Color _color1, Color _color2)
    {
        // 각 색상의 R, G, B 값을 합산하고 두 색상의 평균을 구합니다.
        float r = (_color1.r + _color2.r) / 2;
        float g = (_color1.g + _color2.g) / 2;
        float b = (_color1.b + _color2.b) / 2;

        return new Color(r, g, b, 1); // 알파값은 1로 설정합니다.
    }

    void Start()
    {
        // 색상 정의
        Color color_red = new Color(1, 0, 0); // 빨강
        Color color_green = new Color(0, 1, 0); // 초록
        Color color_blue = new Color(0, 0, 1); // 파랑

        // 색상 혼합
        Color mixedColor1 = BlendColors(color_red, color_green); // 빨강 + 초록
        Color mixedColor2 = BlendColors(color_green, color_blue); // 빨강 + 파랑
        Color mixedColor3 = BlendColors(color_blue, color_red); // 초록 + 파랑

        // 결과 출력
        Debug.Log("빨강 + 초록: " + mixedColor1);
        Debug.Log("빨강 + 파랑: " + mixedColor2);
        Debug.Log("초록 + 파랑: " + mixedColor3);
        
        
    }
    
    // 두 색상을 혼합하여 새로운 색상을 생성하는 함수
    public ColorBasicInfo BlendColors(string colorNo1, string colorNo2)
    {
        return colorTable.MixColors(colorNo1, colorNo2);
    }
    
    private void ColorBlendWithNo()
    {
        // 색상 혼합
        var mixedColor1 = BlendColors("01", "02"); // 예시 색상 번호
        var mixedColor2 = BlendColors("03", "04"); // 예시 색상 번호

        if (mixedColor1 != null)
        {
            Debug.Log("혼합된 색상 1: " + mixedColor1.ColorName + " - " + mixedColor1.ColorValue);
        }

        if (mixedColor2 != null)
        {
            Debug.Log("혼합된 색상 2: " + mixedColor2.ColorName + " - " + mixedColor2.ColorValue);
        }
    }
}
