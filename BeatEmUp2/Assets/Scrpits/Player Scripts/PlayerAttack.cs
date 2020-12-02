using System.Collections;
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
    public Transform leftPunch;

    CharacterAnimation playerAnim;
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
            if (currentComboState == ComboState.PUNCH_3 || currentComboState == ComboState.KICK_1 || currentComboState == ComboState.KICK_2)
                return;
                
                currentComboState++;    //move the combo state from none-punch1(arrayIndex)
                activateTimerToReset = true;
                currentComboTimer = defaultComboTimer;

                switch (currentComboState)
                {
                    case ComboState.PUNCH_1:
                        playerAnim.Punch1();
                        break;
                    case ComboState.PUNCH_2:
                        playerAnim.Punch2();
                        break;
                    case ComboState.PUNCH_3:
                        playerAnim.Punch3();
                        break;
                    
                }

            }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //return because no combos to perform
            if (currentComboState == ComboState.KICK_2 || currentComboState == ComboState.PUNCH_3)
                return;
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
                    playerAnim.Kick1(); //1.25.45 YT
                    break;
                case ComboState.KICK_2:
                    playerAnim.Kick2();
                    break;
            }
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

