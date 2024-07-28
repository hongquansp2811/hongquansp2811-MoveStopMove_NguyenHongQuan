using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBaseScrollViews : UICanvas
{
    public Transform shopContent;
    public UISkinShopItem shopItem;
    public UICShopSkin uICShopSkin;
    public int IDItem;
    public int PriceItem;

    public Button BtnSelect;
    public Button BtnUnlock;
    public Button BtnBuy;
    public TextMeshProUGUI equipedTxt;

    public List<UISkinShopItem> shopItems = new List<UISkinShopItem>();
   
    public virtual void OnInit()
    {
        BtnSelect.onClick.AddListener(EquipItem);
        BtnUnlock.onClick.AddListener(UnlockItem);
        BtnBuy.onClick.AddListener(BuyItem);
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

    public void OnItemSelected(int itemId, int price)
    {
        IDItem = itemId;
        PriceItem = price;
    }

    public void RemoveGlowFromAllItems()
    {
        foreach (var item in shopItems)
        {
            item.ResetGlow();
        }
    }

    public void CloseAllButton()
    {
        BtnSelect.gameObject.SetActive(false);
        BtnUnlock.gameObject.SetActive(false);
        BtnBuy.gameObject.SetActive(false);
        equipedTxt.gameObject.SetActive(false);
    }

    public void UnlockItem()
    {
        UserDataManager.Ins.AddPurchasedItem(GetPurchaseKey(), IDItem);
        EquipItem();
        CheckButton();
    }

    public void BuyItem()
    {
        Debug.Log(PriceItem);
        if (PriceItem > LevelManager.Ins.GetCoint()) return;

        LevelManager.Ins.SetCointPlayer(LevelManager.Ins.GetCoint() - PriceItem);
        UserDataManager.Ins.AddPurchasedItem(GetPurchaseKey(), IDItem);
        EquipItem();
        CheckButton();
    }

    public void EquipItem()
    {
        UserDataManager.Ins.SetSelectedItem(GetSelectedKey(), IDItem);
        UpdateEquippedStatus();
        HandleEquippedItem();
        CheckButton();
    }

    public virtual string GetPurchaseKey()
    {
        return "";
    }

    public virtual string GetSelectedKey()
    {
        return "";
    }

    public void CheckButton()
    {
        if (IsSelectedItem())
        {
            CloseAllButton();
            equipedTxt.gameObject.SetActive(true);
        }
        else if (IsBoughtItem())
        {
            CloseAllButton();
            BtnSelect.gameObject.SetActive(true);
        }
        else
        {
            CloseAllButton();
            BtnBuy.gameObject.SetActive(true);
            BtnUnlock.gameObject.SetActive(true);
        }
    }

    protected virtual void HandleEquippedItem()
    {

    }

    public virtual void DemoItem()
    {

    }

    private bool IsBoughtItem()
    {
        return UserDataManager.Ins.GetPurchasedItems(GetPurchaseKey()).Contains(IDItem);
    }

    private bool IsSelectedItem()
    {
        return UserDataManager.Ins.GetSelectedItem(GetSelectedKey()) == IDItem;
    }
}

public interface IScrollViews
{
    void OnItemSelected(int itemId, int price);
    void RemoveGlowFromAllItems();
    void CheckButton();
}
