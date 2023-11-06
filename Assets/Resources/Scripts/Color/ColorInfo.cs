using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorInfo
{
    [SerializeField] private string colorName;
    [SerializeField] private Color color;

    public string ColorName => colorName;
    public Color Color => color;
    
    public ColorInfo(string _name, Color _color)
    {
        this.colorName = _name;
        this.color = _color;
    }
}