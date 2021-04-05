using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Crowbar : MonoBehaviour
{
    public GameObject player;
    public float damage = 100f;
    public GameObject enemy;

    public TextMeshProUGUI PickUPTxT;
    public LayerMask layerMask;

    public GameObject hitFX;

    float health = 100f;

    private void Awake()
    {
        PickUPTxT.gameObject.SetActive(false);
    }

    private void Update()
    {
        DestroyOBJ();

    }


    /// <summary>
    /// Function which destroys obj based on health
    /// </summary>
    void DestroyOBJ()
    {
        if (health <= 0)
        {
            player.GetComponent<PlayerAttack>().holdingObject = false;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //if the enemy was hit
        if (other.collider.name == "EnemyRig")
        {
            Debug.LogError("CROWBAR");
            
            //Play hit animation on enemy
            other.gameObject.GetComponent<EnemyGreen>().enemyAnim.Hit();

            //instantiate hit fx
            Instantiate(hitFX, other.GetContact(0).point, Quaternion.identity);

            //take dmg and play the hit sound
            enemy.GetComponent<EnemyGreen>().TakeDMG(damage);
            player.GetComponent<Player>().gm.GetComponent<AudioManager>().Play("CrowbarHit", false);

            //Add repelent force
            other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(100, other.GetContact(0).point, 2f);

            //take dmg of weapon
            health -= 25;
        }

        //object pickup
        if(other.collider.tag ==Tags.GROUND_TAG)
        {
            PickUPTxT.gameObject.SetActive(true);
            player.GetComponent<Player>().canPickUPOBJ = true;
        }
      
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.tag == Tags.GROUND_TAG)
        {
            PickUPTxT.gameObject.SetActive(false);
            player.GetComponent<Player>().canPickUPOBJ = false;
        }
    }
}
