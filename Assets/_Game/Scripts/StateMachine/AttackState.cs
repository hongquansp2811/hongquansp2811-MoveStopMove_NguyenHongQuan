using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    private float attackDelay = 1f;
    private float timer;

    public void OnEnter(Bot t)
    {
        t.ChangeAnim(Cache.CACHE_ANIM_ATTACK);
        t.PerformAttack();
        timer = 0;
    }

    public void OnExecute(Bot t)
    {
        timer += Time.deltaTime;
        if (timer >= attackDelay)
        {
            t.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot t)
    {
        t.StopMoving();
    }
}
