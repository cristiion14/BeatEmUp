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
public class PlayerAttack : MonoBehaviour {
    CharacterAnimation playerAnim;
    bool activateTimerToReset;
    float defaultComboTimer = 0.4f;
    float currentComboTimer;

    ComboState currentComboState;
	// Use this for initialization
	void Awake () {
        playerAnim = GetComponentInChildren<CharacterAnimation>();
	}
	
    void Start()
    {
        currentComboTimer = defaultComboTimer;
        currentComboState = ComboState.NONE;
    }

	// Update is called once per frame
	void Update () {
        AnimatePlayerAttack();
        ResetComboState();

    }

    void AnimatePlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            currentComboState++;    //move the combo state from none-punch1(arrayIndex)
            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer;
            
            if(currentComboState == ComboState.PUNCH_1)
            {
                playerAnim.Punch1();
            }

            if (currentComboState == ComboState.PUNCH_2)
            {
                playerAnim.Punch2();
            }

            if (currentComboState == ComboState.PUNCH_3)
            {
                playerAnim.Punch3();
            }

        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            playerAnim.Kick1();
        }
    }

    void ResetComboState()
    {
        if(activateTimerToReset)
        {
            currentComboTimer -= Time.deltaTime;
            if(currentComboTimer<=0)
            {
                //ran out of time 
                currentComboState = ComboState.NONE;
                activateTimerToReset = false;
                currentComboTimer = defaultComboTimer;
            }
        }

    }
}
