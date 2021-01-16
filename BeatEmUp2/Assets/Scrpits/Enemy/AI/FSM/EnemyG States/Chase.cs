using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State<EnemyGreen>
{
    public override void Execute(EnemyGreen agent)
    {
        Debug.LogError("CHASING!");

        agent.agent.SetDestination(agent.player.transform.position);

        if ((agent.player.transform.position - agent.transform.position).sqrMagnitude < 2f)
            agent.ChangeState(new IdleState());
    }
}
