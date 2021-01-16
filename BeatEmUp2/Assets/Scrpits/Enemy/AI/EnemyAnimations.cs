using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    public Animator enemyAnim;

    public void Chase(bool chase)
    {
        enemyAnim.SetBool(EnemyAnim.FOUND_PLAYER, chase);
    }

    public void Attack(bool attack)
    {
        enemyAnim.SetBool(EnemyAnim.CAN_ATTACK, attack);
    }
}
