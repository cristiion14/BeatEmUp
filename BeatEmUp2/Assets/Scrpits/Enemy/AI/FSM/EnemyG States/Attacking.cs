using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : State<EnemyGreen>
{

    public override void Execute(EnemyGreen agent)
    {
        agent.GetComponent<EnemyAttack>().Attack();

        //activate side walls for fighting
        GM.instance.isFighting = true;

        agent.enemyAnim.EnemyWalk(false);

        agent.agent.isStopped = true;
        agent.agent.stoppingDistance = .5f;
        agent.agent.autoBraking = true;

        if (agent.GetComponent<EnemyAttack>().followPlayer)
        {
            GM.instance.isFighting = false;
            agent.ChangeState(new Chase());

        }


        if (agent.GetComponent<EnemyGreen>().playerGB.GetComponent<Player>().GetCurrentHealth() <= 0)
            agent.ChangeState(new IdleState());

      //  if (agent.GetCurrentHealth() <= 0)
        //    agent.ChangeState(new DeathState());

//        Debug.LogError("Attacking!");

    }
  
}
