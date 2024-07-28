using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : UICanvas
{
    public Button BtnPlay;
    public Button BtnShopSkin;
    public Button BtnShopWeapon;
    public Button BtnResetData;
    public Button BtnCheatTest;

    private void Awake()
    {
        BtnPlay.onClick.AddListener(OnClickPlay);
        BtnShopSkin.onClick.AddListener(OnClickOpenShopSkin);
        BtnShopWeapon.onClick.AddListener(OnClickOpenShopWeapon);
        BtnResetData.onClick.AddListener(OnClickResetData);
        BtnCheatTest.onClick.AddListener(OnClickCheatTestGame);
    }

    private void OnClickCheatTestGame()
    {
        GameManager.Ins.ChangeState(GameState.TestGame);
        Close();
    }

    private void OnClickPlay()
    {
        GameManager.Ins.ChangeState(GameState.Gameplay);
        Close();
    }

    private void OnClickOpenShopWeapon()
    {
        UIManager.Ins.OpenUI<UICShopWeapon>();
        Close();
    }

    private void OnClickOpenShopSkin()
    {
        GameManager.Ins.ChangeState(GameState.ShopSkin);
        Close();
    }

    private void OnClickResetData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
