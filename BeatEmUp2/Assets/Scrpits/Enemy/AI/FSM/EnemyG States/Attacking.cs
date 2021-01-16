using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : State<EnemyGreen>
{
    public override void Execute(EnemyGreen agent)
    {
        agent.enemyAnim.Chase(false);
        agent.enemyAnim.Attack(true);   
        Debug.LogError("Attacking!");
    }
}
