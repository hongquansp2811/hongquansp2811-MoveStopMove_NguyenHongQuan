using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICMainMenu : UICanvas
{
    public Button BtnPlay;
    public Button BtnShopSkin;
    public Button BtnShopWeapon;
    public Button BtnResetData;

    private void Awake()
    {
        BtnPlay.onClick.AddListener(OnClickPlay);
        BtnShopSkin.onClick.AddListener(OnClickOpenShopSkin);
        BtnShopWeapon.onClick.AddListener(OnClickOpenShopWeapon);
    }

    private void OnClickOpenShopWeapon()
    {
        
    }

    private void OnClickOpenShopSkin()
    {
        GameManager.Ins.ChangeState(GameState.ShopSkin);
    }

    private void OnClickPlay()
    {
        GameManager.Ins.ChangeState(GameState.Gameplay);
    }
}
