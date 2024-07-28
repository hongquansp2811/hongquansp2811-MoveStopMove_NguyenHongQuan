using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    public void OnEnter(Bot enemy)
    {
        enemy.MoveBot();
    }

    public void OnExecute(Bot enemy)
    {
        if (enemy.charactersInRange.Count > 0)
        {
            enemy.ChangeState(new AttackState());
        }
        else
        {
            enemy.MoveBot();
        }
    }

    public void OnExit(Bot enemy)
    {
        enemy.StopMoving();
    }
}
