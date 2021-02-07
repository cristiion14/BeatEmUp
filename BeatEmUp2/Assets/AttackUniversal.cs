using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUniversal : MonoBehaviour
{
    //Universal script for the attack mechanism of the player and enemy 

    public LayerMask collisionLayer;

    public float radius = 1f;
    public float damage = 10f;

    public bool is_Player, is_Enemy;

    public GameObject hitFX;


    private void Update()
    {
        DetectCollision();
    }

    void DetectCollision()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, collisionLayer);

        if(colliders.Length > 0)
        {
            //has hit something
            Debug.Log("Player hit the: " + colliders[0].gameObject.name);


            //deal damage
            colliders[0].gameObject.GetComponent<EnemyGreen>().TakeDMG(damage);

            //instantiate hit fx
            Instantiate(hitFX, colliders[0].transform.position, Quaternion.identity);

            //deactivate the gameobject
            gameObject.SetActive(false);
        }
    }
}
