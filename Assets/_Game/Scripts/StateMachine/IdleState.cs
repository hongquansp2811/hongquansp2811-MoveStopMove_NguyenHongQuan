using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    private float time;
    private float idleDuration = 1f;

    public void OnEnter(Bot enemy)
    {
        enemy.StopMoving();
        enemy.ChangeAnim(Cache.CACHE_ANIM_IDLE);
        time = 0;
    }

    public void OnExecute(Bot enemy)
    {
        time += Time.deltaTime;
        if (time >= idleDuration)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot enemy)
    {
        enemy.StopMoving();
    }
}
