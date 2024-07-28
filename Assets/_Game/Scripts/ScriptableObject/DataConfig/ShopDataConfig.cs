using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopDataConfig", menuName = "Scriptable Objects/Shop Data Config", order = 1)]
public class ShopDataConfig : ScriptableObject
{
    public List<WeaponData> WeaponDataConfigs = new List<WeaponData>();

    public List<HairData> HairDataConfigs = new List<HairData>();
    public List<Pants> PantsDataConfigs = new List<Pants>();
    public List<Amor> AmorDataConfigs = new List<Amor>();
    public List<SetFull> SetFullDataConfigs = new List<SetFull>();
}

[Serializable]
public class HairData : BaseData
{
   
}

[Serializable]
public class Pants : BaseData
{

}

[Serializable]
public class Amor : BaseData
{

}

[Serializable]
public class SetFull : BaseData
{

}

[Serializable]
public class WeaponData : BaseData
{
    
}

public class BaseData
{
    public int ID;
    public string Name;
    public string Description;
    public int Price;
    public Sprite image;
}
