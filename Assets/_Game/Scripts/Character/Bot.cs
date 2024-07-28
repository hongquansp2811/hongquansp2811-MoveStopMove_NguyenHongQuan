using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

public class Bot : Character
{
    [SerializeField] private float movementSpeed = 3.5f;
    [SerializeField] private NavMeshAgent agent;
    private IState<Bot> currentState;

    private void Start()
    {
        OnInit();
    }

    public override void OnInit()
    {
        base.OnInit();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed;
        currentState = new IdleState();
        currentState.OnEnter(this);
    }

    private void Update()
    {
        if (!isDead)
        {
            BotBehavior();
        }
        SetPointText();
    }

    private void BotBehavior()
    {
        CleanCharactersInRange();
        currentState.OnExecute(this);
    }

    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void StopMoving()
    {
        agent.isStopped = true;
        isMoving = false;
        ChangeAnim(Cache.CACHE_ANIM_IDLE);
    }

    public void MoveBot()
    {
        Character nearestCharacter = FindTarget();
        if (nearestCharacter != null)
        {
            agent.isStopped = false;
            agent.SetDestination(nearestCharacter.TF.position);
            ChangeAnim(Cache.CACHE_ANIM_RUN);
            isMoving = true;
        }
    }

    public Character FindTarget()
    {
        int random = UnityEngine.Random.Range(1, 5);
        Character target = null;

        switch (random)
        {
            case 1:
                target = FindNearestCharacter();
                break;
            case 2:
                target = FindBigCharacter();
                break;
            case 3:
                target = FindPlayer();
                break;
            case 4:
                target = FindFurthestCharacter();
                break;
            default:
                target = FindNearestCharacter();
                break;
        }

        return target;
    }

    public Character FindNearestCharacter()
    {
        Character nearestCharacter = null;
        float minDistance = float.MaxValue;

        foreach (Character character in LevelManager.Ins.currentMap.activeCharacters)
        {
            if (character != this)
            {
                float distance = Vector3.Distance(transform.position, character.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestCharacter = character;
                }
            }
        }

        return nearestCharacter;
    }

    public Character FindWeakestCharacter()
    {
        List<Character> characters = LevelManager.Ins.currentMap.activeCharacters;
        Character weakestCharacter = null;
        int minPoints = int.MaxValue;

        foreach (Character character in characters)
        {
            if (character == this) continue;

            if (character.points < minPoints)
            {
                weakestCharacter = character;
                minPoints = character.points;
            }
        }

        return weakestCharacter;
    }

    public Character FindBigCharacter()
    {
        List<Character> characters = LevelManager.Ins.currentMap.activeCharacters;
        Character maxPointCharacter = null;
        float maxPoints = float.MinValue;
        float minDistance = float.MaxValue;

        foreach (Character character in characters)
        {
            if (character == this) continue;

            if (character.points > maxPoints)
            {
                maxPoints = character.points;
                maxPointCharacter = character;
                minDistance = Vector3.Distance(transform.position, character.transform.position);
            }
            else if (character.points == maxPoints)
            {
                float distance = Vector3.Distance(transform.position, character.transform.position);
                if (distance < minDistance)
                {
                    maxPointCharacter = character;
                    minDistance = distance;
                }
            }
        }

        return maxPointCharacter;
    }

    public Character FindPlayer()
    {
        return LevelManager.Ins.currentMap.player;
    }

    public Character FindFurthestCharacter()
    {
        List<Character> characters = LevelManager.Ins.currentMap.activeCharacters;
        Character furthestCharacter = null;
        float maxDistance = float.MinValue;

        foreach (Character character in characters)
        {
            if (character == this) continue;

            float distance = Vector3.Distance(transform.position, character.transform.position);
            if (distance > maxDistance)
            {
                furthestCharacter = character;
                maxDistance = distance;
            }
        }

        return furthestCharacter;
    }
}
