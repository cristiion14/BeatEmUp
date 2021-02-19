using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAttack : MonoBehaviour
{
    public CharacterAnimation enemyAnim;
    float attackDistance = 1f;
    float chasePlayerAfterAttack = 1f;

    float currentAttackTime;
    float defaultAttackTime = 2f;

   public bool attackPlayer, followPlayer;

    
   public void Attack()
    {
        //if the player isn't supposed to be hit, exit function
        if (!attackPlayer)
            return;


        currentAttackTime += Time.deltaTime;

        if(currentAttackTime>defaultAttackTime)
        {
            enemyAnim.EnemyAttack(Random.Range(0, 3));
            currentAttackTime = 0f;
        }

        if (Vector3.Distance(transform.position, GetComponentInParent<EnemyGreen>().playerGB.transform.position)>attackDistance+chasePlayerAfterAttack)
        {
            attackPlayer = false;
            followPlayer = true;
        }
    }

}
