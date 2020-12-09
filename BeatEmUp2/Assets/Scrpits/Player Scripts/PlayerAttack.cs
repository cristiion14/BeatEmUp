﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComboState
{
    NONE,
    PUNCH_1,
    PUNCH_2,
    PUNCH_3,
    KICK_1,
    KICK_2
}
public class PlayerAttack : MonoBehaviour
{
    public GameObject punchSystem;
    /*
    public Transform leftPunch;
    public Transform rightPunch;
    public Transform leftKick;
    public Transform rightKick;
    */

    public bool isPunching = false;
    public bool isKicking = false;

    float punchAttackTimer = 2f;
    bool hasPressedPunchKeyUp = false;

    float kickAttackTimer = 2f;
    bool hasPressedKickKeyUp = false;


   public CharacterAnimation playerAnim;
    bool activateTimerToReset;
    float defaultComboTimer = 0.4f;
    float currentComboTimer;

    ComboState currentComboState;
    // Use this for initialization
    void Awake()
    {
        playerAnim = GetComponentInChildren<CharacterAnimation>();
    }

    void Start()
    {
        currentComboTimer = defaultComboTimer;
        currentComboState = ComboState.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatePlayerAttack();
        ResetComboState();
    }



    void AnimatePlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            punchAttackTimer = 2;
            hasPressedPunchKeyUp = false;
            if (currentComboState == ComboState.PUNCH_3 || currentComboState == ComboState.KICK_1 || currentComboState == ComboState.KICK_2)
            {
                punchAttackTimer -= Time.deltaTime;
                if (punchAttackTimer <= 1.5f)
                    isPunching = false;

                return;
            }
                
                currentComboState++;    //move the combo state from none-punch1(arrayIndex)
                activateTimerToReset = true;
                currentComboTimer = defaultComboTimer;

                switch (currentComboState)
                {
                    case ComboState.PUNCH_1:
                        isPunching = true;
                        playerAnim.Punch1();
                        break;
                    
                    case ComboState.PUNCH_2:
                        isPunching = true;
                        playerAnim.Punch2();
                        break;
                    case ComboState.PUNCH_3:
                        isPunching = true;
                        playerAnim.Punch3();
                        break;
                    
                }
            }
       
        if(Input.GetKeyUp(KeyCode.J))
        {
            hasPressedPunchKeyUp = true;  
        }

        if(hasPressedPunchKeyUp)
        {
            punchAttackTimer -= Time.deltaTime;
            if (punchAttackTimer <= 1.5f)
                isPunching = false;
        }
       // Debug.LogError("the attack timer is: " + punchAttackTimer);  
        

        if (Input.GetKeyDown(KeyCode.K))
        {
            kickAttackTimer = 2;
            hasPressedPunchKeyUp = false;

            //return because no combos to perform
            if (currentComboState == ComboState.KICK_2 || currentComboState == ComboState.PUNCH_3)
            {
                kickAttackTimer -= Time.deltaTime;
                if (kickAttackTimer <= 1.5f)
                    isKicking = false;

                return;
            }
               
            //start the combo
            if (currentComboState == ComboState.NONE || currentComboState == ComboState.PUNCH_1 || currentComboState == ComboState.PUNCH_2)
            {
                currentComboState = ComboState.KICK_1;
            }
            //go to next state 
            else if (currentComboState == ComboState.KICK_1)
            {
                currentComboState++;
            }
            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer;
            switch(currentComboState)
            {
                case ComboState.KICK_1:
                    isKicking = true;
                    isPunching = false;
                    playerAnim.Kick1(); //1.25.45 YT
                    break;
                case ComboState.KICK_2:
                    isKicking = true;
                    isPunching = false;
                    playerAnim.Kick2();
                    break;
            }
        }
        if(Input.GetKeyUp(KeyCode.K))
        {
            hasPressedKickKeyUp = true;
        }

        if (hasPressedKickKeyUp)
        {
            kickAttackTimer -= Time.deltaTime;
            if (kickAttackTimer <= 1.5f)
               isKicking = false;
        }
        
    }

    void ResetComboState()
    {
        if (activateTimerToReset)
        {
            currentComboTimer -= Time.deltaTime;
            if (currentComboTimer <= 0)
            {
                //ran out of time 
                currentComboState = ComboState.NONE;
                activateTimerToReset = false;
                currentComboTimer = defaultComboTimer;
            }
        }

    }

 
}

