using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UICScrollViewWeapon : UICanvas
{
    public Transform shopContent;
    public UIWeaponShopItem shopItem;
    public List<UIWeaponShopItem> shopItems = new List<UIWeaponShopItem>();

    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        if (shopContent == null || shopItem == null)
        {
            return;
        }

        PopulateShop();
    }

    public void CloseButton()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        Close();
    }

    public void UpdateEquippedStatus()
    {
        foreach (var item in shopItems)
        {
            item.UpdateUI();
        }
    }

    public void PopulateShop()
    {
        for (int i = 0; i < LevelManager.Ins.shopData.WeaponDataConfigs.Count; i++)
        {
            BaseData data = LevelManager.Ins.shopData.WeaponDataConfigs[i];

            if (data == null)
            {
                return;
            }

            UIWeaponShopItem item = Instantiate(shopItem, shopContent);

            if (item == null)
            {
                return;
            }
            shopItems.Add(item);
            item.SetItem(data, this);
        }

        UpdateEquippedStatus();
    }
}
