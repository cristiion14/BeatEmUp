using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : State<EnemyGreen>
{
    Vector3 walkPoint;
    bool walkPointSet;
    float walkPointRange = 60;

    void SearchWalkPoint()
    {

        EnemyGreen enemy = GameObject.Find("EnemyRig").GetComponent<EnemyGreen>();
    
    }
    public override void Execute(EnemyGreen agent)
    {
        Debug.LogError("Patroling!!");


        //calculate random point in range of the player
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);



        //search for a random point on the map
        walkPoint = new Vector3(agent.transform.position.x + randomX, agent.transform.position.y, agent.transform.position.z + randomZ);

        agent.enemyAnim.Attack(false);
        agent.enemyAnim.Chase(true);
        agent.agent.SetDestination(walkPoint);

        if (agent.targetFound())
            agent.ChangeState(new Chase());


    }
}
