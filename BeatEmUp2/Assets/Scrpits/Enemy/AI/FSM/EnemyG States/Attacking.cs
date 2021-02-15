using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : State<EnemyGreen>
{

    public override void Execute(EnemyGreen agent)
    {
        agent.GetComponentInChildren<EnemyAttack>().Attack();

        agent.enemyAnim.EnemyWalk(false);

        agent.agent.isStopped = true;
        agent.agent.stoppingDistance = .5f;
        agent.agent.autoBraking = true;

        if (agent.GetComponentInChildren<EnemyAttack>().followPlayer)
            agent.ChangeState(new Chase());


      //  if (agent.GetCurrentHealth() <= 0)
        //    agent.ChangeState(new DeathState());

        Debug.LogError("Attacking!");

    }
  
}
