using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUniversal : MonoBehaviour
{
    //Universal script for the attack mechanism of the player and enemy 

    public LayerMask collisionLayer;

    public float radius = .5f;
    float damage = 50f;

    public bool is_Player, is_Enemy;

    public GameObject hitFXPrefab;



  public  int comboCounter = 0;
   public float comboTimer = 3f;
    public bool canIncreaseCombo = false;

    GameObject GM;

    private void Start()
    {
        GM = GameObject.Find("GM");
    }

    private void Update()
    {
        DetectCollision();
      //  GM.GetComponent<GM>().comboCounterTXT.text = comboCounter.ToString();

        Debug.Log("reset combo = " + canIncreaseCombo);
   //     GetComponentInParent<PlayerAttack>().resetComboCounter = canIncreaseCombo;

      //  GM.GetComponent<GM>().playerGB.GetComponent<PlayerAttack>().comboCounter = comboCounter;

        if (GM.GetComponent<GM>().playerGB.GetComponentInChildren<PlayerAttack>().resetComboCounter)
            comboCounter = 0;
        
    }



    void DetectCollision()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, collisionLayer);

       if(colliders.Length <= 0 && GM.GetComponent<GM>().playerGB.GetComponentInChildren<PlayerAttack>().comboCounter>0)
        {
            
            GM.GetComponent<GM>().playerGB.GetComponentInChildren<PlayerAttack>().resetComboCounter = true;
            Debug.LogError("0 Colliders");
            
        }


        if (colliders.Length > 0)
        {
            

            //Player Attack
            if (is_Player && colliders[0].gameObject.name == "EnemyRig")
            {
               
                    Debug.Log("The collider is: " + this.name);
                    Debug.Log("And has hit the " + colliders[0].name);

                GM.GetComponent<GM>().playerGB.GetComponentInChildren<PlayerAttack>().comboCounter++;
                GM.GetComponent<GM>().playerGB.GetComponentInChildren<PlayerAttack>().comboResetTimer = 3;
                GM.GetComponent<GM>().playerGB.GetComponentInChildren<PlayerAttack>().resetComboCounter = false;


                //                Debug.LogError("combo counter: " + GM.GetComponent<GM>().playerGB.GetComponentInChildren<PlayerAttack>().comboCounter);
                //comboCounter++;

                /*
                //switch case to set the damage based on where the player hit
                switch (colliders[0].tag)
                {
                    case "Head":
                        {
                            damage = 40;
                            Debug.LogAssertion("Hit to the head");
                            break;
                        }

                    case "Body":
                        {
                            damage = 10;
                            break;
                        }
                }
                */

                if (this.tag == Tags.PLeft_Punch)
                {
                    Debug.Log("A DAT CU STANGU!");
                    damage = 10;
                }
                else
                {
                    damage = 20;
                }
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
            else if(is_Player && colliders[0].gameObject.name != "EnemyRig" && GM.GetComponent<GM>().playerGB.GetComponentInChildren<PlayerAttack>().comboCounter >0)
            {
                Debug.LogError("CRINGEEEEEEEEE");
               // GM.GetComponent<GM>().playerGB.GetComponentInChildren<PlayerAttack>().resetComboCounter = true;
               // canIncreaseCombo = false;
            }


            //Enemy Attack
            if (is_Enemy)
            {
                Debug.Log("The name of the hit item is: " + colliders[0].name);

                Debug.Log("The tag of the hit item is: "+colliders[0].tag);
                //switch case to set the damage based on where the player hit
                switch (colliders[0].tag)
                {
                    case "Head":
                        {
                            damage = 40;
                            Debug.LogError("Hit to the head");
                            break;
                        }

                    case "Body":
                        {
                            damage = 10;
                            break;
                        }
                   // default:
                        //{
                          //  Debug.LogError("DEFAULT");
                        //    damage = 25;
                           // break;
                      //  }

                }

                //play hit animation on player
                colliders[0].gameObject.GetComponentInParent<CharacterAnimation>().HitPlayer();

                //deal damage to player
                colliders[0].gameObject.GetComponentInParent<Player>().TakeDMG(damage);
//                Debug.Log("DAMAGE IS: " + damage);

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
