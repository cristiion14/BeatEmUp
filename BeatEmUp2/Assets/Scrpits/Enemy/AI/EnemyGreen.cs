using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyGreen : MonoBehaviour
{

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
    public float radius = 1.5f;         // enemy's looking radius 


    //health values
    private float initialHealth = 100, currentHealth, maxHealth;
   [SerializeField] float damage = 30;
    float shield, initialShield = 100;

    //Getters and Setters for the health
    public float GetCurrentHealth() { return currentHealth; }
    public void SetCurrentHealth(float newHealth) { currentHealth = initialHealth; }
    public float GetCurrentShield() { return shield; }
    public void SetCurrentShield(float newShield) { shield = newShield; }

    //UI
    public Image healthBar;
    public Image shieldBar;

    //FX
    public GameObject deathFX;

    public LayerMask whatIsGround, whatIsPlayer;

    private void Awake()
    {
        EnemyInitialize();
    }

    public void Start()
    {
        enemyFSM = new Chase();
    }


    void Update()
    {
        enemyFSM.Execute(this);
        UpdateHealthAndShield();

      //  StartCoroutine(Die());
//        Debug.LogError("Enemy's health is: " + currentHealth + " and the shield are: "+shield); 
    }


    void EnemyInitialize()
    {
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
        float distance = (player.transform.position - transform.position).sqrMagnitude;
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


       else if (shield <= 0 && currentHealth > 0)
            currentHealth -= amount;

    }

  public  IEnumerator Die()
    {

        if (currentHealth <= 0)
        {
            print("Enemy has died...");
            yield return new WaitForSeconds(.5f);
            Instantiate(deathFX, transform.position, Quaternion.identity);
            gameObject.SetActive(false);

            yield return new WaitForSeconds(.3f);
            print("Respawning enemy!");
            gameObject.SetActive(true);

            EnemyInitialize();

        }
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
