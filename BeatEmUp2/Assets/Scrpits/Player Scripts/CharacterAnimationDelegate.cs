﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationDelegate : MonoBehaviour
{
    // This class activates the attack points for collision detection of hits between the player and enemy

    public GameObject GM;

    public GameObject leftArmAttackPoint, rightArmAttackPoint, leftKickAttackPoint, rightKickAttackPoint;

    public GameObject enemyLeftArmAttackPoint, enemyRightArmAttackPoint, enemyLeftKickAttackPoint, enemyRightKickAttackPoint;

    public GameObject objetHolder, crowbar;

    Vector3 crobarSnapLocation = new Vector3(0.302f, 1.569f, 0.427f);

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

        rightKickAttackPoint.SetActive(true);
    }

    void rightKickAttackOFF()
    {
        if (rightKickAttackPoint.activeInHierarchy)
            rightKickAttackPoint.SetActive(false);
    }

    void AttackCrowbar()
    {
        crowbar.transform.SetParent(objetHolder.transform);
        crowbar.transform.position = objetHolder.transform.position;
        crowbar.transform.localEulerAngles = new Vector3(-90f, 4.7f, 0f);
        
    }

    void PlayStepSoundR()
    {
        GM.GetComponent<AudioManager>().Play("Steps2", true);
    }

    void PlayStepSound()
    {
        /*
        float nr = Random.Range(1,4);

        switch(nr)
        {
            case 1:
                {
                    GM.GetComponent<AudioManager>().Play("Steps1", true);
                    break;
                }
            case 2:
                {
                    GM.GetComponent<AudioManager>().Play("Steps2", true);
                    break;
                }
            case 3:
                {
                    GM.GetComponent<AudioManager>().Play("Steps3", true);
                    break;
                }
            case 4:
                {
                    GM.GetComponent<AudioManager>().Play("Steps4", true);
                    break;
                }
        }
        */
        GM.GetComponent<AudioManager>().Play("Steps1", true);

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

    void EnemyStepSoundL()
    {

        GM.GetComponent<AudioManager>().Play("EnemySteps1", true, true) ;


    }

    void EnemyStepSoundR()
    {
        GM.GetComponent<AudioManager>().Play("EnemySteps2", true, true);

    }

    #endregion

}
