using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopSkinData", menuName = "Scriptable Objects/Shop Skin Data", order = 1)]
public class ShopSkinData : ScriptableObject
{
    public List<HairSkinObject> hairSkinObjects = new List<HairSkinObject>();
    public List<ShieldSkinObject> shieldSkinObjects = new List<ShieldSkinObject>();

    public HairSkinObject GetHairSkinObjectBuyID(int ID)
    {
        return hairSkinObjects[ID];
    }

    public ShieldSkinObject GetShieldSkinObjectBuyID(int ID)
    {
        return shieldSkinObjects[ID];
    }
}
