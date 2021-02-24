using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUniversal : MonoBehaviour
{
    //Universal script for the attack mechanism of the player and enemy 

    public LayerMask collisionLayer;

    public float radius = .5f;
    float damage = 10f;

    public bool is_Player, is_Enemy;

    public GameObject hitFXPrefab;


    private void Update()
    {
        DetectCollision();
    }

    void DetectCollision()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, collisionLayer);

        if(colliders.Length > 0)
        {
            //Player Attack
            if(is_Player)
            {
                //deal damage to enemy
                colliders[0].gameObject.GetComponent<EnemyGreen>().TakeDMG(damage);

                //find the position to instantiate the effect
                Vector3 hitFXPos = colliders[0].transform.position;
                hitFXPos.y += 1.3f;

                if (colliders[0].transform.forward.x > 0)        //facing right
                    hitFXPos.x += .3f;
                else if (colliders[0].transform.forward.x < 0)      //facing left
                    hitFXPos.x -= .3f;


                //deactivate the running anim of the enemy
                //colliders[0].gameObject.GetComponent<EnemyGreen>().enemyAnim.EnemyWalk(false);
               // colliders[0].gameObject.GetComponent<EnemyGreen>().enemyRB.constraints = RigidbodyConstraints.FreezePosition;
                  colliders[0].gameObject.GetComponent<EnemyGreen>().canMove = false;


                //instantiate hit fx
                Instantiate(hitFXPrefab, hitFXPos, Quaternion.identity);

                //play the hit animation on the enemy
                colliders[0].gameObject.GetComponent<EnemyGreen>().enemyAnim.Hit();

            }

            //Enemy Attack
             if(is_Enemy)
            {

                //play hit animation on player
                colliders[0].gameObject.GetComponentInChildren<CharacterAnimation>().HitPlayer();

                //deal damage to player
                colliders[0].gameObject.GetComponent<Player>().TakeDMG(damage);

                //find the position to instantiate the effect
                Vector3 hitFXPos = colliders[0].transform.position;
                hitFXPos.y += 1.3f;

                if (colliders[0].transform.forward.x > 0)        //facing right
                    hitFXPos.x += .3f;
                else if (colliders[0].transform.forward.x < 0)      //facing left
                    hitFXPos.x -= .3f;

                //instantiate hit fx
                Instantiate(hitFXPrefab, hitFXPos, Quaternion.identity);
            }

            //deactivate the gameobject
            gameObject.SetActive(false);
        }
    }
}
