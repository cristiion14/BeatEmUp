using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGreen : MonoBehaviour
{

    public EnemyAnimations enemyAnim;
   public NavMeshAgent agent;         

    State<EnemyGreen> enemyFSM;          //reference to the enemy's finite state machine
    public PlayerMovement player;       // reference to the player GB
    public float speed = 0.3f;
    public float radius = 1.5f;         // enemy's looking radius 

    public LayerMask whatIsGround, whatIsPlayer;
    
    public void Start()
    {
        enemyFSM = new Patrol();
    }


    void Update()
    {
        enemyFSM.Execute(this);
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
}
