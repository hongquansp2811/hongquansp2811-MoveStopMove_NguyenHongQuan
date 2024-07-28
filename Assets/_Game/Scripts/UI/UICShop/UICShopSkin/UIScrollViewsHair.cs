using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class UIScrollViewsHair : UIBaseScrollViews, IScrollViews
{
    private void Start()
    {
        OnInit();
    }

    public override void OnInit()
    {
        base.OnInit();
        if (shopContent == null || shopItem == null)
        {
            return;
        }

        PopulateShop();
    }

    public void PopulateShop()
    {
        for (int i = 0; i < LevelManager.Ins.shopData.HairDataConfigs.Count; i++)
        {
            BaseData data = LevelManager.Ins.shopData.HairDataConfigs[i];

            if (data == null)
            {
                return;
            }

            UISkinShopItem item = Instantiate(shopItem, shopContent);

            if (item == null)
            {
                return;
            }
            shopItems.Add(item);
            item.SetItem(data, this);
        }

        UpdateEquippedStatus();
    }

    public override string GetPurchaseKey()
    {
        return Cache.HAIR_PURCHASED_KEY;
    }

    public override string GetSelectedKey()
    {
        return Cache.HAIR_SELECTED_KEY;
    }

    protected override void HandleEquippedItem()
    {
        base.HandleEquippedItem();
        LevelManager.Ins.currentMap.player.ChangeSkinHair(IDItem);
    }

    public override void DemoItem()
    {
        base.DemoItem();
        LevelManager.Ins.currentMap.demoCharacter.ChangeSkinHair(IDItem);
    }
}
