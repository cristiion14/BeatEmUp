using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State<EnemyGreen>
{
    public override void Execute(EnemyGreen agent)
    {
        Debug.LogError("CHASING!");

        agent.agent.isStopped = false;

        agent.agent.stoppingDistance = .5f;

        agent.enemyAnim.EnemyWalk(true);
        agent.agent.SetDestination(agent.playerGB.transform.position);

        if (agent.targetFound())
        {
            agent.GetComponentInChildren<EnemyAttack>().followPlayer = false;
            agent.GetComponentInChildren<EnemyAttack>().attackPlayer = true;


            agent.ChangeState(new Attacking());
        }

        

       // if (agent.GetCurrentHealth() <= 0)
     //       agent.ChangeState(new DeathState());
    }
}
