using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationDelegate : MonoBehaviour
{
    public GameObject leftArmAttackPoint, rightArmAttackPoint, leftKickAttackPoint, rightKickAttackPoint;
    
    //turn on the attack points
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
}
