using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "Scriptable Objects/Map Data Scriptable Object", order = 1)]
public class MapDataConfig : ScriptableObject
{
    public int TotalBotInScene;
    public int TotalBotToSpawn;
    public float TimeToPlay;
}
