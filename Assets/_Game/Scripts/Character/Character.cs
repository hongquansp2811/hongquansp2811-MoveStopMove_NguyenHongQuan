using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Character : GameUnit
{
    public List<Character> charactersInRange = new List<Character>();
    public bool isMoving;
    public int points;
    public bool isDead;
    public Equipment equipment;

    public event Action<Character> OnDeath;

    [SerializeField] private AttackZone attackZone;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Animator anim;
    [SerializeField] private string currentAnim;
    [SerializeField] private Transform weaponCharacter;
    [SerializeField] private PointText pointTextPrefab;
    [SerializeField] private GameObject circleTarget;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    private WeaponCharacter weaponPrefab;
    private PointText pointText;
    private Character currentTarget;

    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        points = 1;
        isDead = false;
        anim.SetBool(Cache.CACHE_ANIM_IDLE, true);
        currentAnim = Cache.CACHE_ANIM_IDLE;
        attackZone.SetOwner(this);
        if (pointText != null)
        {
            Destroy(pointText.gameObject);
        }
        pointText = Instantiate(pointTextPrefab, transform.position + new Vector3(0, 3, 0), Quaternion.identity);
        pointText.transform.SetParent(this.TF);
        SetPointText();
        equipment.curentFullMesh = skinnedMeshRenderer.material;
    }

    public void AddCharacterInRange(Character character)
    {
        if (!charactersInRange.Contains(character))
        {
            charactersInRange.Add(character);
            character.OnDeath += HandleCharacterDeath;
        }
    }

    public void RemoveCharacterInRange(Character character)
    {
        if (charactersInRange.Contains(character))
        {
            charactersInRange.Remove(character);
            character.OnDeath -= HandleCharacterDeath;
        }
    }

    public void ActivateCircleTarget(bool isActive)
    {
        if (circleTarget != null)
        {
            circleTarget.SetActive(isActive);
        }
    }

    public Color GetColor()
    {
        return skinnedMeshRenderer.material.color;
    }

    public virtual void Attack()
    {
        if (isDead || charactersInRange.Count <= 0) return;

        CleanCharactersInRange();
        Character target = GetRandomTaget();
        if (target == null) return;

        if (this is Player)
        {
            if (currentTarget != null) currentTarget.ActivateCircleTarget(false);
            currentTarget = target;
            currentTarget.ActivateCircleTarget(true);
        }

        Vector3 direction = (target.TF.position - TF.position).normalized;
        direction.y = 0;
        TF.rotation = Quaternion.LookRotation(direction);
        StartCoroutine(PerformAttackSequence(0.5f, direction, target));
    }

    public void CleanCharactersInRange()
    {
        charactersInRange.RemoveAll(character => character == null || !character.gameObject.activeSelf);
    }

    public Character GetRandomTaget()
    {
        if (charactersInRange.Count == 0)
        {
            return null;
        }
        int randomIndex = UnityEngine.Random.Range(0, charactersInRange.Count);
        return charactersInRange[randomIndex];
    }

    public virtual void Die()
    {
        if (isDead) return;
        isDead = true;
        if (OnDeath != null)
        {
            OnDeath.Invoke(this);
        }
        charactersInRange.Clear();
        ChangeAnim(Cache.CACHE_ANIM_DEATH);
        ResetScale();
        Invoke(nameof(OndDespawn), 1f);
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.SetBool(currentAnim, false);
            currentAnim = animName;
            anim.SetBool(currentAnim, true);
            anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        }
    }

    public override void OndDespawn()
    {
        base.OndDespawn();
        LevelManager.Ins.SpawnCharacter();
    }

    public void PerformAttack()
    {
        Invoke(nameof(Attack), 0.4f);
    }

    public void ChangeSkinnedMeshRendererColor(Material material)
    {
        if (skinnedMeshRenderer != null)
        {
            skinnedMeshRenderer.material = material;
        }
        else
        {
            Debug.LogError("SkinnedMeshRenderer not found in children.");
        }
    }

    public void ChangeWeapon(WeaponCharacterEnum weaponEnum)
    {
        WeaponCharacter newWeapon = LevelManager.Ins.weaponData.GetWeaponCharacter(weaponEnum);
        weaponPrefab = newWeapon;
        equipment.ChangeItem(weaponPrefab, weaponCharacter);
    }

    public void ChangeSkinHair(int ID)
    {
        equipment.ChangeSkinHair(ID);
    }

    public void ChangeSkinShield(int ID)
    {
        equipment.ChangeSkinShield(ID);
    }

    public void ChangePant(Material material)
    {
        equipment.ChangePant(material);
    }

    public void ChangeSetFull(int ID)
    {
        equipment.ChangeSetFull(ID);
    }

    public virtual void AddPoints(Character character)
    {
        if (character != this)
        {
            points += character.points;
            ChangeScale();
        }
    }

    public void SetPointText()
    {
        pointText.OnInit(points);
    }

    private void HandleCharacterDeath(Character character)
    {
        RemoveCharacterInRange(character);
    }

    private IEnumerator PerformAttackSequence(float attackDelay, Vector3 direction, Character target)
    {
        ChangeAnim(Cache.CACHE_ANIM_ATTACK);
        yield return new WaitForSeconds(attackDelay);
        CreateWeapon(direction, target);
        if (this is Player player)
        {
            player.isAttacking = false;
        }
    }

    private void CreateWeapon(Vector3 direction, Character target)
    {
        Quaternion spawnRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(-90, 0, 0);
        Weapon weaponObject = SimplePool.Spawn<Weapon>(weaponPrefab.poolType, attackPoint.position, spawnRotation);
        weaponObject.SetOwner(this);
        weaponObject.SetTargetPosition(target.TF.position);
        weaponObject.onHitCharacter = AddPoints;
    }

    private void ChangeScale()
    {
        this.TF.localScale += new Vector3(0.05f, 0.05f, 0.05f);
    }

    private void ResetScale()
    {
        this.TF.localScale = new Vector3(1f, 1f, 1f);
    }
}
