using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICShopWeapon : UICanvas
{
    public Button CloseButton;
    [SerializeField] private TextMeshProUGUI coinText;


    private void Update()
    {
        UpdateAliveText();
    }

    private void UpdateAliveText()
    {
        coinText.text = LevelManager.Ins.GetCoint().ToString();
    }

    public void OnCloseButton()
    {
        Close();
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }
}
