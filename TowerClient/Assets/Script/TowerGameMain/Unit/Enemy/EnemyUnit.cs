using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyUnit : Unit
{
    public Transform Target;
    public NavMeshAgent agent;
    public int hitCount;
    public const int MaxHitCout = 5;

    public override void Init(Action<GameObject> callBack = null)
    {
        base.Init((go) =>
            {
                if(callBack != null)
                {
                    callBack(go);
                }
                Target = GameObject.Find("End").transform;
                agent = go.AddComponent<NavMeshAgent>();
                agent.SetDestination(Target.position);
                agent.speed = 5.0f;
            }
        );
    }

    public override void Hit(Unit unit)
    {
        base.Hit(unit);
        hitCount += 1;
        if (hitCount > MaxHitCout)
        {
            State = UnitState.Dispose;
        }
    }



    public override void Update(float detalTime)
    {
        base.Update(detalTime);
        if(isDie() == false )
        {
            if(Module != null && Module.go != null)
            {
                CurPos = Module.go.transform.position;
            }
        }
    }
}
