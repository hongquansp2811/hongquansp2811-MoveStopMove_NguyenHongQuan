using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataConfig", menuName = "Scriptable Objects/Player Data Config Scriptable Object", order = 1)]
public class PLayerDataConfig : ScriptableObject
{
    public float moveSpeed;
    public float rotSpeed;
    public float attackCooldown;
    public int coinBonus;
}
