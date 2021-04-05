using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public enum ComboState
{
    NONE,
    PUNCH_1,
    PUNCH_2,
    PUNCH_3,
    KICK_1,
    KICK_2
}

public enum CrowbarComboState
{
    NONE,
    Hit_1,
    Hit_2,
    Hit_3,
}
public class PlayerAttack : MonoBehaviour
{
    PlayerControlls playerControls;

    public GameObject hitEffect;
    public bool isPunching = false;
    public bool isKicking = false;

    bool leftPunchHit = false;
    bool rightKickHit = false;
    public Transform leftPunch;
    public Transform rightKick;
   public GameObject dustEffectLand;

    float punchAttackTimer = 2f;
    bool hasPressedPunchKeyUp = false;

    float kickAttackTimer = 2f;
    bool hasPressedKickKeyUp = false;


    public CharacterAnimation playerAnim;
    bool activateTimerToReset;
    float defaultComboTimer = 0.4f;
    float currentComboTimer;

    public float comboResetTimer = 3;
    public  int comboCounter = 0;
    public bool resetComboCounter = false;

    ComboState currentComboState;
    // Use this for initialization

    //crowbar vars
    public  bool holdingObject = false;
    bool holdingObjectAttack = false;
    bool hasPressedAttackObjUp = false;
    
    CrowbarComboState crowbarComboState;
    bool crowbarActivateTimerToReset = false;
    float crowbarAttackTimer = 2f;
    float CrowbarComboResetTimer = 3f;
    float CrowbarDefaultComboTimer = .4f;
    float crowbarCurrentComboTimer;




    void Awake()
    {
        //reference to the controller
        playerControls = new PlayerControlls();

        //controller actions
        playerControls.Gameplay.Punch.performed += ctx => Punch();
        playerControls.Gameplay.Kick.performed += ctx => Kick();
        playerControls.Gameplay.Movement.performed += ctx => GetComponentInParent<PlayerMovement>().DetectMov();
        playerControls.Gameplay.Jump.performed += ctx => Jump();
        playerControls.Gameplay.Block.performed += ctx => Defend();

        playerAnim = GetComponentInChildren<CharacterAnimation>();
    }

    void Start()
    {
        currentComboTimer = defaultComboTimer;
        currentComboState = ComboState.NONE;

        crowbarCurrentComboTimer = CrowbarDefaultComboTimer;
        crowbarComboState = CrowbarComboState.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        if (!holdingObject)
            AnimatePlayerAttack();
        else
        {
            //call the crowbar attack function
            CrowbarAttack();

            //drop item
            if(Input.GetKeyDown(KeyCode.P))
            {
                holdingObject = false;
                GetComponentInChildren<CharacterAnimationDelegate>().crowbar.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                GetComponentInChildren<CharacterAnimationDelegate>().crowbar.GetComponent<Rigidbody>().useGravity = true;
                GetComponentInChildren<CharacterAnimationDelegate>().crowbar.GetComponent<Rigidbody>().isKinematic = false;

                GetComponentInChildren<CharacterAnimationDelegate>().crowbar.transform.SetParent(null);
            }
        }
        ResetComboState();



        if (isPunching || isKicking || holdingObjectAttack)
        {
             // GetComponentInParent<PlayerMovement>().enemy.canMove = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;


        }



        else if (!isPunching && !isKicking && !holdingObjectAttack)
        {
            // movement constraints reset
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            // GetComponentInChildren<AttackUniversal>().canIncreaseCombo = false;
            resetComboCounter = true;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        }


        if(resetComboCounter)
        {
//            Debug.LogError("COMBO FALSE!");
            comboResetTimer -= Time.deltaTime;
            if (comboResetTimer <= 0)
            {
                comboCounter = 0;
                comboResetTimer = 3;
                resetComboCounter = false;


            }
            //   ComboReset();
        }

        GetComponentInParent<Player>().gm.GetComponent<GM>().comboCounterTXT.text = comboCounter.ToString(); //+" \n    "+ comboResetTimer.ToString();


        //  Defend();
    }

    void CrowbarAttack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        { 
            crowbarAttackTimer = 2f;
            
            if(crowbarComboState == CrowbarComboState.Hit_3)
            {
                crowbarAttackTimer -= Time.deltaTime;
                if (crowbarAttackTimer <= 1.5f)
                {
                    crowbarComboState = CrowbarComboState.NONE;
                    holdingObjectAttack = false;
                }
                return;
                    
            }
            
            Debug.LogError(crowbarComboState);

            crowbarComboState++;
            crowbarActivateTimerToReset = true;
            crowbarCurrentComboTimer = CrowbarDefaultComboTimer;

            switch (crowbarComboState)
            {
                case CrowbarComboState.Hit_1:
                    {
                        holdingObjectAttack = true;
                        playerAnim.CrowbarHit1();
                        break;
                    }

                case CrowbarComboState.Hit_2:
                    {
                        holdingObjectAttack = true;
                        playerAnim.CrowbarHit2();
                        break;
                    }

                case CrowbarComboState.Hit_3:
                    {
                        holdingObjectAttack = true;
                        playerAnim.CrowbarHit3();
                        break;
                    }
            
            }

        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            hasPressedAttackObjUp = true;
            holdingObjectAttack = false;
        }

        if(hasPressedAttackObjUp)
        {
            crowbarAttackTimer -= Time.deltaTime;
            if (crowbarAttackTimer <= 1.5f)
                crowbarComboState = CrowbarComboState.NONE;
        }
    }

    #region Controller Function
    void ComboReset()
    {
        //combo reset
        //float timer = GetComponentInChildren<AttackUniversal>().comboTimer;

        float timer = 3;
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            comboCounter = 0;
        }
    }

    void OnEnable()
    {
        playerControls.Gameplay.Enable();
    }

    void Jump()
    {
        //set the velocity of the rb on the Y axis to the jump value
        Vector3 vel = GetComponentInParent<Rigidbody>().velocity;
        vel.y = 4;
        GetComponentInParent<Rigidbody>().velocity = vel;

        playerAnim.Jump();
    }

    void Kick()
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
        switch (currentComboState)
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

    void Punch()
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
                GetComponent<Player>().gm.GetComponent<AudioManager>().Play("Hit", false);

                break;

                case ComboState.PUNCH_2:
                    isPunching = true;
                    playerAnim.Punch2();
                GetComponent<Player>().gm.GetComponent<AudioManager>().Play("Hit", false);

                break;
                case ComboState.PUNCH_3:
                    isPunching = true;
                    playerAnim.Punch3();
                GetComponent<Player>().gm.GetComponent<AudioManager>().Play("Hit", false);

                break;
            }

    }

    void PunchUP()
    {
        hasPressedPunchKeyUp = true;

        punchAttackTimer -= Time.deltaTime;
        if (punchAttackTimer <= 1.5f)
            isPunching = false;
    }
    void Defend()
    {
        if (Input.GetKey(KeyCode.Q))
            playerAnim.Defend();
    }

    #endregion


    void AnimatePlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.LogError("Normal Combo state: " + currentComboState);
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

        //Ground Punch
        if (Input.GetKeyDown(KeyCode.Z))
        {
            leftPunchHit = true;
            rightKickHit = false;
            playerAnim.GroundPunch();
        }

        //Ground Kick
        if (Input.GetKeyDown(KeyCode.X))
        {
            rightKickHit = true;
            leftPunchHit = false;
            playerAnim.GroundKick();
        }

        //Jump Kick
        if (Input.GetKeyDown(KeyCode.C))
            playerAnim.JumpKick();

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

    void ShowDustEffectLand()
    {
        if (leftPunchHit)
            Instantiate(dustEffectLand, leftPunch);
        else
            Instantiate(dustEffectLand, rightKick);
            
    }

  
}

