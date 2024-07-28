using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AttackZone : MonoBehaviour
{
    private Character owner;

    public void SetOwner(Character character)
    {
        owner = character;
    }

    void OnTriggerEnter(Collider other)
    {
        Character character = Cache.GetComponentFromCache<Character>(other);
        if (character != null && character != owner && other.CompareTag(Cache.CACHE_TAG_CHARACTER))
        {
            owner.AddCharacterInRange(character);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character != null && character != owner && other.CompareTag(Cache.CACHE_TAG_CHARACTER))
        {
            owner.RemoveCharacterInRange(character);
        }
    }
}
