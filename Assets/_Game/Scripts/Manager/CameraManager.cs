using UnityEngine;

public enum CameraStage
{
    ShopSkin, GamePlay
}

public class CameraManager : Singleton<CameraManager>
{
    public CameraFollow cameraFollow;
    private CameraStage cameraStage;

    private void Start()
    {
        LevelManager.Ins.OnLevelInitialized.AddListener(OnLevelInitialized);
    }

    private void OnLevelInitialized()
    {
        ChangeState(CameraStage.GamePlay);
    }

    public void ChangeState(CameraStage cameraStage)
    {
        this.cameraStage = cameraStage;
        switch (this.cameraStage)
        {
            case CameraStage.ShopSkin:
                HandleShopSkinStage();
                break;
            case CameraStage.GamePlay:
                HandleGamePlayStage();
                break;
        }
    }

    private void HandleShopSkinStage()
    {
        if (LevelManager.Ins.currentMap != null && LevelManager.Ins.currentMap.demoCharacter != null)
        {
            cameraFollow.player = LevelManager.Ins.currentMap.demoCharacter;
            cameraFollow.offset = new Vector3(0, 5, -10);
        }
        else
        {
            return;
        }
    }

    private void HandleGamePlayStage()
    {
        if (LevelManager.Ins.currentMap != null && LevelManager.Ins.currentMap.player != null)
        {
            cameraFollow.player = LevelManager.Ins.currentMap.player;
            cameraFollow.offset = new Vector3(0, 25, -20);
        }
        else
        {
            return;
        }
    }
}
