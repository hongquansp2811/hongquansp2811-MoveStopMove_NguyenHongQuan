using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScrollViewsSetFull : UIBaseScrollViews, IScrollViews
{
    public TextMeshProUGUI TxtDescription;

    private void Start()
    {
        OnInit();
    }

    public override void OnInit()
    {
        TxtDescription.gameObject.SetActive(false);
        base.OnInit();
        if (shopContent == null || shopItem == null)
        {
            return;
        }

        PopulateShop();
    }

    public void PopulateShop()
    {
        for (int i = 0; i < LevelManager.Ins.shopData.SetFullDataConfigs.Count; i++)
        {
            BaseData data = LevelManager.Ins.shopData.SetFullDataConfigs[i];

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
        return Cache.SETFULL_PURCHASED_KEY;
    }

    public override string GetSelectedKey()
    {
        return Cache.SETFULL_SELECTED_KEY;
    }

    protected override void HandleEquippedItem()
    {
        base.HandleEquippedItem();
        LevelManager.Ins.currentMap.player.ChangeSetFull(IDItem);
        LevelManager.Ins.currentMap.player.OnInit();
    }

    public override void DemoItem()
    {
        base.DemoItem();
        LevelManager.Ins.currentMap.demoCharacter.ChangeSetFull(IDItem);
    }
}
