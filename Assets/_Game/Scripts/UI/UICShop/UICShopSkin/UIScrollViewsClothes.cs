using UnityEngine;

public class UIScrollViewsClothes : UIBaseScrollViews, IScrollViews
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
        for (int i = 0; i < LevelManager.Ins.shopData.PantsDataConfigs.Count; i++)
        {
            BaseData data = LevelManager.Ins.shopData.PantsDataConfigs[i];

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
        return Cache.PANT_PURCHASED_KEY;
    }

    public override string GetSelectedKey()
    {
        return Cache.PANT_SELECTED_KEY;
    }

    protected override void HandleEquippedItem()
    {
        base.HandleEquippedItem();
        Material material = LevelManager.Ins.pantsData.GetMaterial(IDItem);
        LevelManager.Ins.currentMap.player.ChangePant(material);
    }

    public override void DemoItem()
    {
        base.DemoItem();
        Material material = LevelManager.Ins.pantsData.GetMaterial(IDItem);
        LevelManager.Ins.currentMap.demoCharacter.ChangePant(material);
    }
}
