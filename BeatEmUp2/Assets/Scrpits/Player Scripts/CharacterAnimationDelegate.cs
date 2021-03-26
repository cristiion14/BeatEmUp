using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationDelegate : MonoBehaviour
{
    // This class activates the attack points for collision detection of hits between the player and enemy


    public GameObject leftArmAttackPoint, rightArmAttackPoint, leftKickAttackPoint, rightKickAttackPoint;

    public GameObject enemyLeftArmAttackPoint, enemyRightArmAttackPoint, enemyLeftKickAttackPoint, enemyRightKickAttackPoint;

    #region Turn on the attack points for player
    void LeftArmAttackON()
    {
        leftArmAttackPoint.SetActive(true);
    }

    void LeftArmAttackOFF()
    {
        if (leftArmAttackPoint.activeInHierarchy == true)
            leftArmAttackPoint.SetActive(false);
    }

    void RightArmAttackON()
    {
        rightArmAttackPoint.SetActive(true);
    }

    void RightArmAttackOFF()
    {
        if (rightArmAttackPoint.activeInHierarchy == true)
            rightArmAttackPoint.SetActive(false);
    }

    void LeftKickAttackON()
    {
        leftKickAttackPoint.SetActive(true);
    }

    void LeftKickAttackOFF()
    {
        if (leftKickAttackPoint.activeInHierarchy)
            leftKickAttackPoint.SetActive(false);
    }

    void rightKickAttackON()
    {
        Debug.LogError("SHOULD TURN IT ON");
        rightKickAttackPoint.SetActive(true);
    }

    void rightKickAttackOFF()
    {
        if (rightKickAttackPoint.activeInHierarchy)
            rightKickAttackPoint.SetActive(false);
    }
    #endregion

    #region Turn on attack points for enemy

    void EnemyLHAttackPointON()
    {
        enemyLeftArmAttackPoint.SetActive(true);
    }

    void EnemyLHAttackPointOFF()
    {
        if (enemyLeftArmAttackPoint.activeInHierarchy)
            enemyLeftArmAttackPoint.SetActive(false);
    }


    void EnemyRHAttackPointON()
    {
        enemyRightArmAttackPoint.SetActive(true);
    }

    void EnemyRHAttackPointOFF()
    {
        if (enemyRightArmAttackPoint.activeInHierarchy)
            enemyRightArmAttackPoint.SetActive(false);
    }



    void EnemyLKAttackPointON()
    {
        enemyLeftKickAttackPoint.SetActive(true);
    }

    void EnemyLKAttackPointOFF()
    {
        if (enemyLeftKickAttackPoint.activeInHierarchy)
            enemyLeftKickAttackPoint.SetActive(false);
    }


    void EnemyRKAttackPointON()
    {
        enemyRightKickAttackPoint.SetActive(true);
    }

    void EnemyRKAttackPointOFF()
    {
        if (enemyRightKickAttackPoint.activeInHierarchy)
            enemyRightKickAttackPoint.SetActive(false);
    }


    #endregion

}
