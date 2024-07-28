using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponShopItem : UIBaseShopItem
{
    public Button BtnSelect;
    public Button BtnUnLock;
    public Button BtnBuy;
    public TextMeshProUGUI equipedTxt;

    private BaseData baseData;
    private UICScrollViewWeapon parentScrollView;

    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        BtnSelect.onClick.AddListener(EquipItem);
        BtnUnLock.onClick.AddListener(UnlockItem);
        BtnBuy.onClick.AddListener(BuyItem);
        CheckButton();
    }

    public void SetItem(BaseData newData, UICScrollViewWeapon scrollViews)
    {
        if (newData == null || scrollViews == null) return;
        baseData = newData;
        parentScrollView = scrollViews;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (baseData == null) return;
        ID = baseData.ID;
        ImgIcon.sprite = baseData.image;
        if (Name != null && Description != null && Price != null)
        {
            Name.text = baseData.Name;
            Description.text = baseData.Description;
            Price.text = baseData.Price.ToString();
        }
    }
    public void UnlockItem()
    {
        UserDataManager.Ins.AddPurchasedItem(GetPurchaseKey(), ID);
        EquipItem();
        CheckButton();
    }

    public void BuyItem()
    {
        if (baseData.Price > LevelManager.Ins.GetCoint()) return;
        LevelManager.Ins.SetCointPlayer(LevelManager.Ins.GetCoint() - baseData.Price);
        UserDataManager.Ins.AddPurchasedItem(GetPurchaseKey(), ID);
        EquipItem();
        CheckButton();
    }

    public void EquipItem()
    {
        UserDataManager.Ins.SetSelectedItem(GetSelectedKey(), ID);
        parentScrollView.UpdateEquippedStatus();
        HandleEquippedItem();
        CheckButton();
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
            BtnUnLock.gameObject.SetActive(true);
        }
    }

    public void CloseAllButton()
    {
        BtnSelect.gameObject.SetActive(false);
        BtnUnLock.gameObject.SetActive(false);
        BtnBuy.gameObject.SetActive(false);
        equipedTxt.gameObject.SetActive(false);
    }

    private bool IsBoughtItem()
    {
        return UserDataManager.Ins.GetPurchasedItems(GetPurchaseKey()).Contains(ID);
    }

    private bool IsSelectedItem()
    {
        return UserDataManager.Ins.GetSelectedItem(GetSelectedKey()) == ID;
    }

    public string GetPurchaseKey()
    {
        return Cache.WEAPON_PURCHASED_KEY;
    }

    public string GetSelectedKey()
    {
        return Cache.WEAPON_SELECTED_KEY;
    }

    protected void HandleEquippedItem()
    {
        LevelManager.Ins.currentMap.player.ChangeWeapon((WeaponCharacterEnum)ID);
    }
}
