using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State<EnemyGreen>
{
    public override void Execute(EnemyGreen agent)
    {
        Debug.LogError("STAM");

        if (Input.GetKeyDown(KeyCode.P))
            agent.ChangeState(new Chase());

        if (agent.canMove)
            agent.ChangeState(new Chase());
    }
}
