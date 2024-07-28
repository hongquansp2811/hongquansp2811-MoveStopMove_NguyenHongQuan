using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISCrollViewsAmor : UIBaseScrollViews, IScrollViews
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
        for (int i = 0; i < LevelManager.Ins.shopData.AmorDataConfigs.Count; i++)
        {
            BaseData data = LevelManager.Ins.shopData.AmorDataConfigs[i];

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
        return Cache.AMOR_PURCHASED_KEY;
    }

    public override string GetSelectedKey()
    {
        return Cache.AMOR_SELECTED_KEY;
    }

    protected override void HandleEquippedItem()
    {
        base.HandleEquippedItem();
        LevelManager.Ins.currentMap.player.ChangeSkinShield(IDItem);
    }

    public override void DemoItem()
    {
        base.DemoItem();
        LevelManager.Ins.currentMap.demoCharacter.ChangeSkinShield(IDItem);
    }
}
