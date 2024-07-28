using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PantsData", menuName = "Scriptable Objects/Pants Data", order = 1)]
public class PantsData : ScriptableObject
{
    public List<Material> materials = new List<Material>();

    public Material GetMaterial(int ID)
    {
        return materials[ID];
    }
}
