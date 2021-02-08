using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State<EnemyGreen>
{
    public override void Execute(EnemyGreen agent)
    {
        Debug.LogError("CHASING!");

        agent.enemyAnim.EnemyWalk(true);
        agent.agent.SetDestination(agent.player.transform.position);

        if (agent.targetFound())
        {
            agent.GetComponentInChildren<EnemyAttack>().followPlayer = false;
            agent.GetComponentInChildren<EnemyAttack>().attackPlayer = true;

            agent.ChangeState(new Attacking());
        }
    }
}
