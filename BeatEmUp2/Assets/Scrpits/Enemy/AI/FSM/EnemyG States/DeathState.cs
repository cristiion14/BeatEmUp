using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State<EnemyGreen>
{
    public override void Execute(EnemyGreen agent)
    {
     //   agent.enemyAnim.Death();

       // if (!agent.enemyAnim.anim.GetCurrentAnimatorStateInfo(0).IsName(AnimationTags.DEATH_TRIGGER))
         //   agent.ChangeState(new Chase());


    }
}
