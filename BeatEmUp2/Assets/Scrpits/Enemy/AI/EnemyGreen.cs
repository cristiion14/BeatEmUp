using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyGreen : MonoBehaviour
{
    public Transform initialPos;

    public EnemyAnimations enemyAnim;
    public NavMeshAgent agent;         

    State<EnemyGreen> enemyFSM;          //reference to the enemy's finite state machine
    public PlayerMovement player;       // reference to the player GB
    public float speed = 0.3f;
    public float radius = 1.5f;         // enemy's looking radius 


    //health values
    private float initialHealth = 100, currentHealth, maxHealth;
   [SerializeField] float damage = 30;
    float shield = 100f;

    //Getters and Setters for the health
    public float GetCurrentHealth() { return currentHealth; }
    public void SetCurrentHealth(float newHealth) { currentHealth = initialHealth; }
    public float GetCurrentShield() { return shield; }
    public void SetCurrentShield(float newShield) { shield = newShield; }

    //UI
    public Image healthBar;
    public Image shieldBar;


    public LayerMask whatIsGround, whatIsPlayer;

    private void Awake()
    {
        EnemyInitialize();
    }

    public void Start()
    {
        enemyFSM = new IdleState();
    }


    void Update()
    {
        enemyFSM.Execute(this);
        UpdateHealthAndShield();

//        Debug.LogError("Enemy's health is: " + currentHealth + " and the shield are: "+shield); 
    }


    void EnemyInitialize()
    {
        //Spawn player to initial position
        transform.position = initialPos.position;

        //Set Health
        currentHealth = initialHealth;

        //Assign health and armour sprite fill amount to the player's health
        healthBar.fillAmount = currentHealth / 100;

        shieldBar.fillAmount = shield / 100;

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
