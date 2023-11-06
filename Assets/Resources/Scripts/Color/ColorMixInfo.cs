using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorMixInfo
{
    [SerializeField] private string mixName; // 섞인 색상의 이름
    [SerializeField] private List<string> baseColorNames; // 섞을 기본 색상들의 이름
    [SerializeField] private Color resultColor; // 결과로 나오는 색상

    public string MixName => mixName;
    public List<string> BaseColorNames => baseColorNames;
    public Color ResultColor => resultColor;

    public ColorMixInfo(string _mixName, List<string> _baseColorNames, Color _resultColor)
    {
        mixName = _mixName;
        baseColorNames = _baseColorNames;
        resultColor = _resultColor;
    }
}