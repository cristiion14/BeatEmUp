using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State<EnemyGreen>
{
    public override void Execute(EnemyGreen agent)
    {
        Debug.LogError("CHASING!");

        agent.enemyAnim.Attack(false);
        agent.enemyAnim.Chase(true);
        agent.agent.SetDestination(agent.player.transform.position);

  //      if (agent.targetFound())
    //        agent.ChangeState(new Attacking());
    }
}
