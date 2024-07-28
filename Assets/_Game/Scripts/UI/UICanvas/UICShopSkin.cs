using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICShopSkin : UICanvas
{
    public Button BtnShopSkinHair;
    public Button BtnShopSkinClothes;
    public Button BtnShopSkinAmor;
    public Button BtnShopSkinSetFull;
    public Button CloseButton;

    public UIScrollViewsHair uIScrollViewsHair;
    public UISCrollViewsAmor uIScrollViewsAmor;
    public UIScrollViewsClothes uIScrollViewsClothes;
    public UIScrollViewsSetFull uIScrollViewsSetFull;

    [SerializeField] private TextMeshProUGUI coinText;


    private void Awake()
    {
        CloseButton.onClick.AddListener(OnCloseButton);

        BtnShopSkinHair.onClick.AddListener(OnBtnShopSkinHairClick);
        BtnShopSkinClothes.onClick.AddListener(OnBtnShopSkinClothesClick);
        BtnShopSkinAmor.onClick.AddListener(OnBtnShopSkinAmorClick);
        BtnShopSkinSetFull.onClick.AddListener(OnBtnShopSkinSetFullClick);
    }

    private void Update()
    {
        UpdateAliveText();
    }

    private void DelDemo()
    {
        LevelManager.Ins.currentMap.demoCharacter.OndDespawn();
        CameraManager.Ins.ChangeState(CameraStage.GamePlay);
    }

    private void UpdateAliveText()
    {
        coinText.text = LevelManager.Ins.GetCoint().ToString();
    }


    private void OnCloseButton()
    {
        Close();
        DelDemo();
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }

    private void OnBtnShopSkinHairClick()
    {
        CloseAll();
        uIScrollViewsHair.gameObject.SetActive(true);
    }

    private void OnBtnShopSkinClothesClick()
    {
        CloseAll();
        uIScrollViewsClothes.gameObject.SetActive(true);
    }

    private void OnBtnShopSkinAmorClick()
    {
        CloseAll();
        uIScrollViewsAmor.gameObject.SetActive(true);
    }

    private void OnBtnShopSkinSetFullClick()
    {
        CloseAll();
        uIScrollViewsSetFull.gameObject.SetActive(true);
    }

    private void CloseAll()
    {
        uIScrollViewsHair.gameObject.SetActive(false);
        uIScrollViewsAmor.gameObject.SetActive(false);
        uIScrollViewsClothes.gameObject.SetActive(false);
        uIScrollViewsSetFull.gameObject.SetActive(false);
    }
}
