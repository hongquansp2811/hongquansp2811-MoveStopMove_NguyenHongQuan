using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorData", menuName = "Scriptable Objects/Color Data Scriptable Object", order = 1)]
public class ColorData : ScriptableObject
{
    public List<Material> colors;
    public Material GetColorData(ColorEnum brickColor)
    {
        return colors[(int)brickColor];
    }
}

public enum ColorEnum
{
    None = 0,
    Red = 1,
    Blue = 2,
    Green = 3,
    Yellow = 4,
    Orange = 5,
    Violet = 6,
}