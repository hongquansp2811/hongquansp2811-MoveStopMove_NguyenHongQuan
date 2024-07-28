using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : Singleton<UserDataManager>
{
    public List<int> GetPurchasedItems(string key)
    {
        string savedData = PlayerPrefs.GetString(key, string.Empty);
        if (string.IsNullOrEmpty(savedData))
        {
            return new List<int>();
        }

        string[] items = savedData.Split(',');
        List<int> itemIds = new List<int>();

        foreach (var item in items)
        {
            if (int.TryParse(item, out int itemId))
            {
                itemIds.Add(itemId);
            }
        }

        return itemIds;
    }

    public void AddPurchasedItem(string key, int itemId)
    {
        List<int> items = GetPurchasedItems(key);
        if (!items.Contains(itemId))
        {
            items.Add(itemId);
            SavePurchasedItems(key, items);
        }
    }

    private void SavePurchasedItems(string key, List<int> items)
    {
        string savedData = string.Join(",", items);
        PlayerPrefs.SetString(key, savedData);
        PlayerPrefs.Save();
    }

    public int GetSelectedItem(string key)
    {
        return PlayerPrefs.GetInt(key, -1);
    }

    public void SetSelectedItem(string key, int itemId)
    {
        PlayerPrefs.SetInt(key, itemId);
        PlayerPrefs.Save();
    }

    public List<int> GetPurchasedHairItems() => GetPurchasedItems(Cache.HAIR_PURCHASED_KEY);
    public void AddPurchasedHairItem(int itemId) => AddPurchasedItem(Cache.HAIR_PURCHASED_KEY, itemId);
    public int GetSelectedHairItem() => GetSelectedItem(Cache.HAIR_SELECTED_KEY);
    public void SetSelectedHairItem(int itemId) => SetSelectedItem(Cache.HAIR_SELECTED_KEY, itemId);

    public List<int> GetPurchasedPantItems() => GetPurchasedItems(Cache.PANT_PURCHASED_KEY);
    public void AddPurchasedPantItem(int itemId) => AddPurchasedItem(Cache.PANT_PURCHASED_KEY, itemId);
    public int GetSelectedPantItem() => GetSelectedItem(Cache.PANT_SELECTED_KEY);
    public void SetSelectedPantItem(int itemId) => SetSelectedItem(Cache.PANT_SELECTED_KEY, itemId);

    public List<int> GetPurchasedAmorItems() => GetPurchasedItems(Cache.AMOR_PURCHASED_KEY);
    public void AddPurchasedAmorItem(int itemId) => AddPurchasedItem(Cache.AMOR_PURCHASED_KEY, itemId);
    public int GetSelectedAmorItem() => GetSelectedItem(Cache.AMOR_SELECTED_KEY);
    public void SetSelectedAmorItem(int itemId) => SetSelectedItem(Cache.AMOR_SELECTED_KEY, itemId);

    public List<int> GetPurchasedSetFullItems() => GetPurchasedItems(Cache.SETFULL_PURCHASED_KEY);
    public void AddPurchasedSetFullItem(int itemId) => AddPurchasedItem(Cache.SETFULL_PURCHASED_KEY, itemId);
    public int GetSelectedSetFullItem() => GetSelectedItem(Cache.SETFULL_SELECTED_KEY);
    public void SetSelectedSetFullItem(int itemId) => SetSelectedItem(Cache.SETFULL_SELECTED_KEY, itemId);

    public List<int> GetPurchasedWeaponItems() => GetPurchasedItems(Cache.WEAPON_PURCHASED_KEY);
    public void AddPurchasedWeaponItem(int itemId) => AddPurchasedItem(Cache.WEAPON_PURCHASED_KEY, itemId);
    public int GetSelectedWeaponItem() => GetSelectedItem(Cache.WEAPON_SELECTED_KEY);
    public void SetSelectedWeaponItem(int itemId) => SetSelectedItem(Cache.WEAPON_SELECTED_KEY, itemId);
}
