using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    public Color curColor;
    private ColorManager colorManager;

    public void Init(ColorManager _colorMng)
    {
        this.colorManager = _colorMng;
        ResetColor();
    }

    public void MixPlayerColor(params Color[] newColors)
    {
        Color[] colorsToMix = new Color[newColors.Length + 1];
        colorsToMix[0] = curColor;
        System.Array.Copy(newColors, 0, colorsToMix, 1, newColors.Length);
        curColor = colorManager.MixColorsTowardsBlack(colorsToMix);
    }

    public void ResetColor()
    {
        curColor = Color.white;
    }
}
