using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGreen : MonoBehaviour
{
    State<EnemyGreen> enemyFSM;          //reference to the enemy's finite state machine
    public List<Node> enemyPath;
    public GameObject grid;

    public PlayerMovement player;

    public float speed = 0.3f;

    
    public void Start()
    {
        enemyFSM = new Chase();
    }


    void Update()
    {
        enemyFSM.Execute(this);
    }
}
