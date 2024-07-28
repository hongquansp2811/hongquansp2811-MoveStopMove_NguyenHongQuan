using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UISkinShopItem : UIBaseShopItem
{
    public Button BtnSelect;
    public Image ImgLock;
    public Image ImgLine;
    [SerializeField] private Image background; 

    private BaseData baseData;
    private UIBaseScrollViews parentScrollView;
    private Color originalColor;

    public void Start()
    {
        if (background != null)
        {
            originalColor = background.color;
        }

        ImgLine.gameObject.SetActive(false);
    }

    public void SetItem(BaseData newData, UIBaseScrollViews scrollViews)
    {
        if (newData == null || scrollViews == null) return;
        baseData = newData;
        parentScrollView = scrollViews;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if ( baseData == null ) return;
        ID = baseData.ID;
        ImgIcon.sprite = baseData.image;
        if (Name != null && Description != null && Price != null)
        {
            Name.text = baseData.Name;
            Description.text = baseData.Description;
            Price.text = baseData.Price.ToString();
        }
        CheckButtons();
    }

    private void CheckButtons()
    {
        bool isBought = UserDataManager.Ins.GetPurchasedItems(parentScrollView.GetPurchaseKey()).Contains(baseData.ID);
        bool isSelected = UserDataManager.Ins.GetSelectedItem(parentScrollView.GetSelectedKey()) == baseData.ID;

        if (isSelected)
        {
            ImgLock.gameObject.SetActive(false);
            HighlightItem();
        }
        else if (isBought)
        {
            ImgLock.gameObject.SetActive(false);
            ResetGlow();
        }
        else
        {
            ImgLock.gameObject.SetActive(true);
            ResetGlow();
        }
    }

    public void OnBtnSelectClick()
    {
        if (parentScrollView is UIScrollViewsSetFull setFullScrollView)
        {
            setFullScrollView.TxtDescription.gameObject.SetActive(true);
            setFullScrollView.TxtDescription.text = baseData.Description;
        }
        parentScrollView.OnItemSelected(baseData.ID, baseData.Price);
        parentScrollView.CheckButton();
        parentScrollView.DemoItem();
        EffectOnclickButton();
    }

    private void EffectOnclickButton()
    {
        parentScrollView.RemoveGlowFromAllItems();
        HighlightItem();
    }

    public void ResetGlow()
    {
        if (background != null)
        {
            background.color = originalColor;
        }
        ImgLine.gameObject.SetActive(false);
    }

    private void HighlightItem()
    {
        if (background != null)
        {
            background.color = Color.white;
        }
        ImgLine.gameObject.SetActive(true);
    }
}
