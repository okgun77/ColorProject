using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ColorManager : MonoBehaviour
{
    public List<Color> baseColors;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        // 다른 클래스들의 Init 함수 호출
        NPCColor[] npcColors = FindObjectsOfType<NPCColor>();
        foreach (var npc in npcColors)
        {
            npc.Init();
        }
        
        FindObjectOfType<PlayerColor>().Init(this);
        FindObjectOfType<TargetColor>().Init();
        FindObjectOfType<ColorComparison>().Init();
    }
    
    
    public Color MixColorsTowardsBlack(params Color[] _colors)
    {
        if (_colors.Length == 0) return Color.white;

        float r = 1;
        float g = 1;
        float b = 1;

        foreach (Color color in _colors)
        {
            r *= color.r;
            g *= color.g;
            b *= color.b;
        }

        return new Color(r, g, b, 1);
    }
}
