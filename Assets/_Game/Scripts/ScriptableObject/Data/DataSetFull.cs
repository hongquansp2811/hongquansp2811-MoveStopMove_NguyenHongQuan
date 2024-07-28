using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataSetFull", menuName = "Scriptable Objects/Data SetFull", order = 1)]

public class DataSetFull : ScriptableObject
{
    public List<SetFullObject> setFullObjects = new List<SetFullObject>();

    public SetFullObject GetSetFullObjectBuyID(int ID)
    {
        return setFullObjects[ID];
    }
}

[Serializable]
public class SetFullObject
{
    public HairSkinObject moduleHair; 
    public ShieldSkinObject moduleShield; 
    public ModuleSetFull moduleTail; 
    public ModuleSetFull moduleWing; 
    public Material paintMesh; 
    public Material fullMesh;

    public BuffType buffType;
    public float value;
}

public enum BuffType
{
    BuffGold,
    BuffRange,
    BuffMoveSpeed
}