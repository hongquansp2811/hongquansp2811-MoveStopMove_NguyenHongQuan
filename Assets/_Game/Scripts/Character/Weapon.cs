using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : GameUnit
{
    public UnityAction<Character> onHitCharacter;

    [SerializeField] private float speed = 10.0f;
    private Vector3 targetPosition;
    private Character owner;
    private bool returning = false;

    private void Update()
    {
        MoveWeapon();
        RotateWeapon();
        DespawnWeapon();
    }

    private void MoveWeapon()
    {
        Vector3 newPosition;

        if (this.poolType == PoolType.Boomerang && returning)
        {
            newPosition = Vector3.MoveTowards(TF.position, owner.transform.position, speed * Time.deltaTime);
        }
        else
        {
            newPosition = Vector3.MoveTowards(TF.position, targetPosition, speed * Time.deltaTime);
        }

        TF.position = new Vector3(newPosition.x, 0.5f, newPosition.z);
    }

    private void RotateWeapon()
    {
        if (this.poolType == PoolType.Bullet_1 || this.poolType == PoolType.Bullet_2)
        {
            TF.Rotate(Vector3.back, 800f * Time.deltaTime, Space.Self);
        }
        else if (this.poolType == PoolType.Boomerang)
        {
            TF.Rotate(Vector3.forward, 800f * Time.deltaTime, Space.Self);
        }
    }

    private void DespawnWeapon()
    {
        if (this.poolType == PoolType.Boomerang)
        {
            if (!returning && Vector3.Distance(TF.position, targetPosition) < 0.1f)
            {
                returning = true;  // Bắt đầu quay về
            }
            else if (returning)
            {
                StartCoroutine(DespawnAfterDelay(2.0f));
            }
        }
        else if (Vector3.Distance(TF.position, targetPosition) < 0.1f)
        {
            OndDespawn();
        }
    }

    public void SetOwner(Character character)
    {
        owner = character;
    }

    public void SetTargetPosition(Vector3 newTargetPosition)
    {
        targetPosition = new Vector3(newTargetPosition.x, 0.5f, newTargetPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        ColliderWithCharacter(other);
    }

    private void ColliderWithCharacter(Collider other)
    {
        if (!other.gameObject.CompareTag(Cache.CACHE_TAG_CHARACTER)) return;
        Character character = Cache.GetComponentFromCache<Character>(other);
        Player player = LevelManager.Ins.currentMap.player;
        if (character != null && character != owner)
        {
            if (character is Player)
            {
                if (player.isImmortal)
                    return;
                else
                    LevelManager.Ins.OnFailGame();
            }
            if (onHitCharacter != null)
            {
                onHitCharacter.Invoke(character);
            }
            character.Die();
            if (owner is Player)
            {
                AudioManager.Ins.PlayDeathSound();
            }
        }
    }

    private IEnumerator DespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        OndDespawn();
        returning = false;
    }
}
