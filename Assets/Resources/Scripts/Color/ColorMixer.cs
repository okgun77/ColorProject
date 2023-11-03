using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMixer : MonoBehaviour
{
    public Color RGBtoCMYK(Color _rgb)
    {
        float c = 1 - _rgb.r;
        float m = 1 - _rgb.g;
        float y = 1 - _rgb.b;
        float k = Mathf.Min(c, m, y);
        if (k < 1)
        {
            c = (c - k) / (1 - k);
            m = (m - k) / (1 - k);
            y = (y - k) / (1 - k);
        }
        else
        {
            c = 0;
            m = 0;
            y = 0;
        }
        return new Color(c, m, y, k);
    }

    public Color CMYKtoRGB(Color _cmyk)
    {
        float r = 255 * (1 - _cmyk.r) * (1 - _cmyk.a);
        float g = 255 * (1 - _cmyk.g) * (1 - _cmyk.a);
        float b = 255 * (1 - _cmyk.b) * (1 - _cmyk.a);
        return new Color(r, g, b);
    }

    public Color MixColors(Color _cmyk1, Color _cmyk2)
    {
        float c = (_cmyk1.r + _cmyk2.r) / 2;
        float m = (_cmyk1.g + _cmyk2.g) / 2;
        float y = (_cmyk1.b + _cmyk2.b) / 2;
        float k = (_cmyk1.a + _cmyk2.a) / 2;
        return new Color(c, m, y, k);
    }
}
