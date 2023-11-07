using TMPro.EditorUtilities;
using UnityEngine;

[System.Serializable]
public class ColorBasicInfo
{
    [SerializeField] private string colorCode;
    [SerializeField] private string colorNo;
    [SerializeField] private string colorName;
    [SerializeField] private Color colorValue;

    public string ColorCode => colorCode;
    public string ColorNo => colorNo;
    public string ColorName => colorName;
    public Color ColorValue => colorValue;

    // RGB 정수 값을 사용하는 생성자
    public ColorBasicInfo(string _code, string _no, string _name, int _r, int _g, int _b)
    {
        colorCode = _code;
        colorNo = _no;
        colorName = _name;
        colorValue = new Color(_r / 255f, _g / 255f, _b / 255f);
    }

    // 기존의 Color 타입을 사용하는 생성자를 유지할 수 있음.
    public ColorBasicInfo(string _code, string _no, string _name, Color _value)
    {
        colorCode = _code;
        colorNo = _no;
        colorName = _name;
        colorValue = _value;
    }
}