using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyGreen : MonoBehaviour
{


    public bool canMove = true;                //used to stop the enemy from moving when attacking

    //Attack vars
    float attackDistance = 1f;
    float chasePlayerAfterAttack = 1f;

    float currentAttackTime;
    float defaultAttackTime = 2f;

    bool attackPlayer, followPlayer;


    public CharacterAnimation enemyAnim;

    public Transform initialPos;

    //public EnemyAnimations enemyAnim;
    public NavMeshAgent agent;         

    State<EnemyGreen> enemyFSM;          //reference to the enemy's finite state machine
    public PlayerMovement player;       // reference to the player GB
    public float speed = 0.3f;
    public float radius = 1f;         // enemy's looking radius 

    public GameObject playerGB;

    //health values
    private float initialHealth = 100, currentHealth, maxHealth;
   [SerializeField] float damage = 30;
    float shield, initialShield = 100;
    bool activateDeathAnim = false;

    //Getters and Setters for the health
    public float GetCurrentHealth() { return currentHealth; }
    public void SetCurrentHealth(float newHealth) { currentHealth = initialHealth; }
    public float GetCurrentShield() { return shield; }
    public void SetCurrentShield(float newShield) { shield = newShield; }

    //UI
    public Image healthBar;
    public Image shieldBar;

    //FX
    public GameObject deathFX, reviveFX;

    public LayerMask whatIsGround, whatIsPlayer;

    private void Awake()
    {
        EnemyInitialize();
       
    }

    public void Start()
    {
        playerGB = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);

        enemyFSM = new Chase();
    }


    void Update()
    {
        if (GetCurrentHealth() > 0)
        {
            enemyFSM.Execute(this);
        }

        UpdateHealthAndShield();

    }


    void EnemyInitialize()
    {
     //   GetComponent<Collider>().enabled = true;


        enemyAnim.Play_IddleAnim();

        //Spawn player to initial position
        transform.position = initialPos.position;

        //Set Health
        currentHealth = initialHealth;

        shield = initialShield;



    }


    public void ChangeState(State<EnemyGreen> newState)
    {
        enemyFSM = newState;
    }

    public bool targetFound()
    {
        float distance = (playerGB.transform.position - transform.position).sqrMagnitude;
        if (distance <= radius * radius)
            return true;
        else
            return false;
    }

    void UpdateHealthAndShield()
    {
        //Update the sprite fill amount
        healthBar.fillAmount = currentHealth / 100;
        shieldBar.fillAmount = shield / 100;
    }

    public void TakeDMG(float amount)
    {
        //take damage first from the shield
        if (shield > 0)
            shield -= amount;

        //take dmg from current health

       else if (shield <= 0 && currentHealth > 0)
            currentHealth -= amount;


        //daeth of enemy object
        if (currentHealth <= 0)
            // play enemy death courutine
            StartCoroutine(Die());
    }

    public IEnumerator Die()
    {
        //play sound
        player.GetComponent<Player>().gm.GetComponent<AudioManager>().Play("Enemy Death", false);

        //instantiate death FX
        Instantiate(deathFX, transform.position, Quaternion.identity);

        //deactivate the collider
        GetComponent<Collider>().enabled = false;

        enemyAnim.Death();
        yield return new WaitForSeconds(3.25f);

        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;

        yield return new WaitForSeconds(.5f);

        //instantiate revive fx
        Instantiate(reviveFX, initialPos);

        //set gb to active
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;


        EnemyInitialize();

    }


    public void Attack()
    {
        //if the player isn't supposed to be hit, exit function
        if (!attackPlayer)
            return;

        currentAttackTime += Time.deltaTime;

        if (currentAttackTime > defaultAttackTime)
        {
            enemyAnim.EnemyAttack(Random.Range(0, 3));
            currentAttackTime = 0f;
        }

        if (Vector3.Distance(transform.position, GetComponent<EnemyGreen>().player.transform.position) > attackDistance + chasePlayerAfterAttack)
        {
            attackPlayer = false;
            followPlayer = true;
        }
    }


    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == Tags.PLAYER_TAG)
        {
            Debug.Log("Collision with " + collision.collider.name);
            player.GetComponentInParent<Player>().TakeDMG(damage);

        }
    }
    */
}
