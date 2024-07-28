using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "HairDataConfig", menuName = "Scriptable Objects/Hair Data Config", order = 1)]
public class HairDataConfig : ScriptableObject
{
    public List<HairData> DataConfigs = new List<HairData>();
}