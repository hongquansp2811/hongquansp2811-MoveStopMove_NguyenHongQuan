using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
    public List<Character> activeCharacters = new List<Character>();
    public Player player;
    public MapDataConfig DataConfig;
    public Bot demoCharacter;
    public CameraFollow cameraFollow;
    public string timeText;

    public event Action OnInitialized;

    [SerializeField] private Player playerPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform demoCharPos;

    private int aliveCount;
    private int totalBotInScene;
    private int totalBotInLevel;
    private bool isTiming;
    private float timeRemaining;

    public int TotalBotInScene
    {
        get => totalBotInScene;
        set => totalBotInScene = value;
    }

    public int TotalBotInLevel
    {
        get => totalBotInLevel;
        set => totalBotInLevel = value;
    }


    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        TotalBotInScene = activeCharacters.Count - 1;
        if (CheckPlayerWin())
        {
            GameManager.Ins.ChangeState(GameState.Win);
            isTiming = false;
        }
        if (isTiming)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimeText();
            if (timeRemaining <= 0)
            {
                GameManager.Ins.ChangeState(GameState.Fail);
                isTiming = false;
            }
        }
    }

    public void OnInit()
    {
        totalBotInScene = 0;
        totalBotInLevel = 0;
        aliveCount = DataConfig.TotalBotToSpawn + 1;
        SpawnPlayer();
        SpawnBot();
        CameraManager.Ins.ChangeState(CameraStage.GamePlay);

        if (player != null)
            OnInitialized?.Invoke();
        else
            Debug.LogError("Player is not set.");

        if (DataConfig.TimeToPlay > 0)
        {
            timeRemaining = DataConfig.TimeToPlay;
            isTiming = true;
            UpdateTimeText();
        }
        else
        {
            isTiming = false;
            timeText = "";
        }
    }

    private void UpdateTimeText()
    {
        if (timeText != null)
        {
            timeText = $"Time: {Mathf.Ceil(timeRemaining)}s";
        }
    }

    public void SpawnDemoCharacter()
    {
        demoCharacter = SimplePool.Spawn<Bot>(PoolType.Bot, demoCharPos.position, Quaternion.identity);
        demoCharacter.TF.eulerAngles = new Vector3(0, 180, 0);
        LevelManager.Ins.SetRandomColorBot(demoCharacter);
        demoCharacter.OnInit();
        demoCharacter.ChangeAnim(Cache.CACHE_ANIM_DANCE);
    }

    public bool CanSpawnBot()
    {
        return TotalBotInScene < DataConfig.TotalBotInScene && TotalBotInLevel < DataConfig.TotalBotToSpawn;
    }

    public bool CheckPlayerWin()
    {
        return TotalBotInLevel >= DataConfig.TotalBotToSpawn && TotalBotInScene == 0 && player != null && !player.isDead;
    }

    
    public int GetAliveCount()
    {
        return aliveCount;
    }

    public void ResetMap()
    {
        if (player != null)
        {
            Destroy(player.gameObject);
        }
    }

    public void HandleCharacterDeath(Character character)
    {
        if (!activeCharacters.Contains(character))
        {
            return;
        }
        activeCharacters.Remove(character);
        aliveCount--;
    }

    public Vector3 GetRandomSpawnPosition() => spawnPoints[Random.Range(0, spawnPoints.Length)].position + RandomVector3();

    private void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, startPos.position, Quaternion.identity);
        if (player != null)
        {
            player.OnDeath += HandleCharacterDeath;
            activeCharacters.Add(player);
            if (cameraFollow != null)
            {
                cameraFollow.SetPlayer(player);
            }
        }
    }

    private void SpawnBot()
    {
        for (int i = 0; i < DataConfig.TotalBotInScene; i++)
        {
            LevelManager.Ins.SpawnCharacter();
        }
    }

    private Vector3 RandomVector3()
    {
        int x = Random.Range(-10, 10);
        int z = Random.Range(-10, 10);
        return new Vector3(x, 0, z);
    }
}
