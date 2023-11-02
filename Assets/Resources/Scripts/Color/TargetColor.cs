using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetColor : MonoBehaviour
{
    public List<Color> targetColors;
    public Color curTargetColor;

    public void Init()
    {
        SetRandomTargetColor();
    }
    
    public void SetRandomTargetColor()
    {
        int randomIndex = Random.Range(0, targetColors.Count);
        curTargetColor = targetColors[randomIndex];
    }

    public Color GetCurrentTargetColor()
    {
        return curTargetColor;
    }
}
