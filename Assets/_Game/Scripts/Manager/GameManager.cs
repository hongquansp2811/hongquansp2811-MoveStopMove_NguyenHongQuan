using System;
using UnityEngine;

public enum GameState
{
    MainMenu, Gameplay, Pause, Win, Fail,
    ShopSkin,
    TestGame
}

public class GameManager : Singleton<GameManager>
{
    private GameState gameState;

    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState gameState)
    {
        this.gameState = gameState;
        switch (this.gameState)
        {
            case GameState.MainMenu:
                HandleMainMenuStage();
                break;
            case GameState.Gameplay:
                HandleGamePlayStage();
                break;
            case GameState.Pause:
                HandlePauseStage();
                break;
            case GameState.Win:
                HandleWinStage();
                break;
            case GameState.Fail:
                HandleFailStage();
                break;
            case GameState.ShopSkin:
                HandleShopSkinState();
                break;
            case GameState.TestGame:
                HandleTestGameState();
                break;
        }
    }

    private void HandleTestGameState()
    {
        Player player = LevelManager.Ins.currentMap.player;
        if (player != null)
        {
            player.isImmortal = true;
        }
        UIManager.Ins.OpenUI<Gameplay>();
        Time.timeScale = 1f;
    }

    private void HandleShopSkinState()
    {
        Time.timeScale = 0f;
        UIManager.Ins.OpenUI<UICShopSkin>();
        LevelManager.Ins.currentMap.SpawnDemoCharacter();
        CameraManager.Ins.ChangeState(CameraStage.ShopSkin);
    }

    private void HandleMainMenuStage()
    {
        Time.timeScale = 0f;
        CameraManager.Ins.ChangeState(CameraStage.GamePlay);
        UIManager.Ins.CloseUI<Gameplay>();
        UIManager.Ins.OpenUI<MainMenu>();
    }

    private void HandleWinStage()
    {
        Time.timeScale = 0f;
        UIManager.Ins.OpenUI<Victory>();
        AudioManager.Ins.PlayWinSound();
    }

    private void HandleFailStage()
    {
        Time.timeScale = 0f;
        UIManager.Ins.CloseUI<Gameplay>();
        UIManager.Ins.OpenUI<Fail>();
        AudioManager.Ins.PlayLoseSound();
    }

    private void HandlePauseStage()
    {
        Time.timeScale = 0f;
        UIManager.Ins.OpenUI<Settings>();
    }

    private void HandleGamePlayStage()
    {
        Player player = LevelManager.Ins.currentMap.player;
        if (player != null)
        {
            player.isImmortal = false;
        }
        UIManager.Ins.OpenUI<Gameplay>();
        Time.timeScale = 1f;
    }

    public bool IsState(GameState gameState)
    {
        return this.gameState == gameState;
    }
}
