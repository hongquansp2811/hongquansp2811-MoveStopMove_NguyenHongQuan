using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataWeapon", menuName = "Scriptable Objects/Data Weapon", order = 1)]
public class DataWeapon : ScriptableObject
{
    public List<WeaponCharacter> weaponCharList = new List<WeaponCharacter>();

    public WeaponCharacter GetWeaponCharacter(WeaponCharacterEnum @enum)
    {
        return weaponCharList[(int)@enum];
    }
}

public enum WeaponCharacterEnum
{
    Candy = 0,
    Arrow = 1,
    Axe = 2,
    Hammer = 3,
}
