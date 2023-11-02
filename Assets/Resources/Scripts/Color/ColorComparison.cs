using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorComparison : MonoBehaviour
{
    private TargetColor targetColor;
    private PlayerColor playerColor;

    public void Init()
    {
        targetColor = FindObjectOfType<TargetColor>();
        playerColor = FindObjectOfType<PlayerColor>();
    }

    public bool IsPlayerColorMatchingTarget()
    {
        return playerColor.curColor == targetColor.GetCurrentTargetColor();
    }
}
