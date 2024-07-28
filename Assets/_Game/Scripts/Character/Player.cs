using System.Collections;
using UnityEngine;

public class Player : Character
{
    public PLayerDataConfig pLayerDataConfig;
    public bool isImmortal = false;
    private float lastAttackTime = -9999f;
    private int coin;
    public bool isAttacking = false;
    private int coinBonus;
    private float moveSpeed;


    private void Awake()
    {
        coin = PlayerPrefs.GetInt(Cache.PLAYERPREF_COIN, 0);
    }

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        HandleInput();
        SetPointText();

    }

    public override void OnInit()
    {
        base.OnInit();
        coinBonus = pLayerDataConfig.coinBonus;
        moveSpeed = pLayerDataConfig.moveSpeed;
        RestoreEquippedSetFull();
        RestoreEquippedPant();
        RestoreEquippedHair();
        RestoreEquippedShield();
        RestoreEquippedWeapon();
    }


    public int GetCoin() { return coin; }

    public void SetCoin(int coin) { this.coin = coin; }

    public override void Die()
    {
        if (isImmortal) return;
        base.Die();
        AudioManager.Ins.PlayDeathSound();
    }

    private void HandleInput()
    {
        Vector3 direction = Joystick.direct;
        if (direction.magnitude >= 0.1f)
        {
            MovePlayer(direction);
            isAttacking = false;
        }
        else
        {
            isMoving = false;
            if (charactersInRange.Count > 0 && Time.time >= lastAttackTime + pLayerDataConfig.attackCooldown)
            {
                isAttacking = true;
                Attack();
                AudioManager.Ins.PlayAttackSound();
                lastAttackTime = Time.time;
            }
            else
            {
                StartCoroutine(DelayedIdle(0.3f));
            }
        }
        if (isAttacking)
        {
            return;
        }
    }

    private IEnumerator DelayedIdle(float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeAnim(Cache.CACHE_ANIM_IDLE);
    }

    private void RestoreEquippedPant()
    {
        int selectedId = UserDataManager.Ins.GetSelectedPantItem();
        if (selectedId != -1)
        {
            Material material = LevelManager.Ins.pantsData.GetMaterial(selectedId);
            ChangePant(material);
        }
    }

    private void RestoreEquippedHair()
    {
        int selectedId = UserDataManager.Ins.GetSelectedHairItem();
        if (selectedId != -1)
        {
            ChangeSkinHair(selectedId);
        }
    }

    private void RestoreEquippedShield()
    {
        int selectedId = UserDataManager.Ins.GetSelectedAmorItem();
        if (selectedId != -1)
        {
            ChangeSkinShield(selectedId);
            coinBonus += coinBonus / 5;
        }
    }

    private void RestoreEquippedSetFull()
    {
        int selectedId = UserDataManager.Ins.GetSelectedSetFullItem();
        if (selectedId != -1)
        {
            SetFullObject setFull = LevelManager.Ins.dataSetFull.GetSetFullObjectBuyID(selectedId);
            ChangeSetFull(selectedId);
            ApplyBuff(setFull.buffType, setFull.value);
            RemoveAllEquippedItems();
        }
    }

    private void ApplyBuff(BuffType buffType, float value)
    {
        switch (buffType)
        {
            case BuffType.BuffGold:
                coinBonus += Mathf.RoundToInt(coinBonus * value / 100);
                break;
            case BuffType.BuffRange:
                // Logic để tăng range của Player
                break;
            case BuffType.BuffMoveSpeed:
                moveSpeed += moveSpeed * value / 100;
                break;
        }
    }

    private void RemoveAllEquippedItems()
    {
        PlayerPrefs.DeleteKey(Cache.HAIR_SELECTED_KEY);
        PlayerPrefs.DeleteKey(Cache.AMOR_SELECTED_KEY);
        PlayerPrefs.Save();
    }


    private void RestoreEquippedWeapon()
    {
        int selectedId = UserDataManager.Ins.GetSelectedWeaponItem();
        if (selectedId != -1)
        {
            ChangeWeapon((WeaponCharacterEnum)selectedId);
        }
    }

    private void MovePlayer(Vector3 direction)
    {
        TF.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), pLayerDataConfig.rotSpeed * Time.deltaTime);
        TF.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        isMoving = true;
        ChangeAnim(Cache.CACHE_ANIM_RUN);
    }

    public override void AddPoints(Character character)
    {
        base.AddPoints(character);
        ChangeCamera();
        AddCoin(coinBonus);
    }

    private void ChangeCamera()
    {
        CameraManager.Ins.cameraFollow.offset.y += 1;
    }

    private void AddCoin(int amount)
    {
        coin += amount;
        PlayerPrefs.SetInt(Cache.PLAYERPREF_COIN, coin);
        PlayerPrefs.Save();
    }
}
