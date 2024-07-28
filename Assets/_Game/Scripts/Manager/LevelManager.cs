using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class LevelManager : Singleton<LevelManager>
{
    public UnityEvent OnLevelInitialized;

    public DataSetFull dataSetFull;
    public ColorData colorData;
    public PantsData pantsData;
    public ShopSkinData shopSkinData;
    public ShopDataConfig shopData;
    public DataWeapon weaponData;
    public List<Map> maps = new List<Map>();
    public Map currentMap;

    private int levelIndex;

    private void Awake()
    {
        levelIndex = PlayerPrefs.GetInt("Level", 1);
    }

    private void Start()
    {
        LoadLevel(levelIndex);
        OnInit();
    }

    public void OnInit()
    {
        OnMainMenuGame();
    }

    public void LoadLevel(int level)
    {
        if (currentMap != null)
        {
            currentMap.ResetMap();
            Destroy(currentMap.gameObject);
        }

        level = (level - 1) % maps.Count + 1;

        if (level > 0 && level <= maps.Count)
        {
            GameObject newMapObject = Instantiate(maps[level - 1].gameObject);
            currentMap = newMapObject.GetComponent<Map>();
            currentMap.OnInitialized += () => OnLevelInitialized?.Invoke();
        }
        else
        {
            Debug.LogError("Invalid level index");
        }
    }


    public void OnStartGame()
    {
        GameManager.Ins.ChangeState(GameState.Gameplay);
    }

    public void OnFinishGame()
    {
        GameManager.Ins.ChangeState(GameState.Win);
    }

    public void OnFailGame()
    {
        GameManager.Ins.ChangeState(GameState.Fail);
    }

    public void OnPauseGame()
    {
        GameManager.Ins.ChangeState(GameState.Pause);
    }

    public void OnMainMenuGame()
    {
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }

    public int GetCoint()
    {
        return currentMap.player.GetCoin();
    }

    public void SetCointPlayer(int newCoint)
    {
        currentMap.player.SetCoin(newCoint);
        PlayerPrefs.SetInt(Cache.PLAYERPREF_COIN, newCoint);
        PlayerPrefs.Save();
    }

    public void OnReset()
    {
        SimplePool.CollectAll();
    }

    internal void OnRetry()
    {
        OnReset();
        LoadLevel(levelIndex);
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }

    internal void OnNextLevel()
    {
        levelIndex++;
        PlayerPrefs.SetInt("Level", levelIndex);
        PlayerPrefs.Save();
        OnReset();
        LoadLevel(levelIndex);
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }

    internal void SpawnCharacter()
    {
        if (!currentMap.CanSpawnBot())
        {
            return;
        }
        Vector3 spawnPoint = currentMap.GetRandomSpawnPosition();
        Bot newBot = SimplePool.Spawn<Bot>(PoolType.Bot, spawnPoint, Quaternion.identity);
        if (newBot == null) return;
        SetRandomColorBot(newBot);
        SetRandomWeaponBot(newBot);
        newBot.OnInit();

        newBot.OnDeath -= currentMap.HandleCharacterDeath;
        newBot.OnDeath += currentMap.HandleCharacterDeath;

        currentMap.TotalBotInScene++;
        currentMap.TotalBotInLevel++;
        currentMap.activeCharacters.Add(newBot);
    }

    private T RandomEnumValue<T>() where T : Enum
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(Random.Range(1, values.Length));
    }

    public void SetRandomColorBot(Bot newBot)
    {
        ColorEnum randomColor = RandomEnumValue<ColorEnum>();
        if ((int)randomColor < colorData.colors.Count)
        {
            newBot.ChangeSkinnedMeshRendererColor(colorData.GetColorData(randomColor));
        }
    }

    private void SetRandomWeaponBot(Bot newBot)
    {
        WeaponCharacterEnum randomWeapon = RandomEnumValue<WeaponCharacterEnum>();
        if ((int)randomWeapon < weaponData.weaponCharList.Count)
        {
            newBot.ChangeWeapon(randomWeapon);
        }
    }
}
