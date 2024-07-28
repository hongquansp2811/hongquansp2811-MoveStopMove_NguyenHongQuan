using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache
{
    public const string CACHE_TAG_CHARACTER = "Character";
    public const string CACHE_TAG_WEAPON = "Weapon";
    public const string CACHE_ANIM_IDLE = "IsIdle";
    public const string CACHE_ANIM_RUN = "IsRun";
    public const string CACHE_ANIM_ATTACK = "IsAttack";
    public const string CACHE_ANIM_DEATH = "IsDead";
    public const string CACHE_ANIM_DANCE = "IsDance";

    public const string HAIR_PURCHASED_KEY = "HairPurchased";
    public const string PANT_PURCHASED_KEY = "PantPurchased";
    public const string AMOR_PURCHASED_KEY = "AmorPurchased";
    public const string SETFULL_PURCHASED_KEY = "SetFullPurchased";
    public const string WEAPON_PURCHASED_KEY = "WeaponPurchased";

    public const string HAIR_SELECTED_KEY = "HairSelected";
    public const string PANT_SELECTED_KEY = "PantSelected";
    public const string AMOR_SELECTED_KEY = "AmorSelected";
    public const string SETFULL_SELECTED_KEY = "SetFullSelected";
    public const string WEAPON_SELECTED_KEY = "WeaponSelected";

    public const string PLAYERPREF_COIN = "coin";

    private static Dictionary<Collider, object> cache = new Dictionary<Collider, object>();

    public static T GetComponentFromCache<T>(Collider collider) where T : Component
    {
        if (collider == null) return null;

        if (!cache.ContainsKey(collider))
        {
            T component = collider.GetComponent<T>();
            if (component != null)
            {
                cache.Add(collider, component);
            }
        }

        return cache.ContainsKey(collider) ? cache[collider] as T : null;
    }
}
