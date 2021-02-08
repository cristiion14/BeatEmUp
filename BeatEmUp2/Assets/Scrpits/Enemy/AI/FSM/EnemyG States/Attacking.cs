using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : State<EnemyGreen>
{

    public override void Execute(EnemyGreen agent)
    {
        agent.GetComponentInChildren<EnemyAttack>().Attack();

        if (agent.GetComponentInChildren<EnemyAttack>().followPlayer)
            agent.ChangeState(new Chase());
        Debug.LogError("Attacking!");

    }
  
}
