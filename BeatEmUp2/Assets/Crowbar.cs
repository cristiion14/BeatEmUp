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

    private void Awake()
    {
        PickUPTxT.gameObject.SetActive(false);
    }

    private void Update()
    {
        
        /*
        //catch the nearby colliders
        Collider[] nearbyColliders=  Physics.OverlapSphere(transform.position, 1, layerMask);
        if (nearbyColliders.Length > 0)
        {
            foreach (Collider col in nearbyColliders)
            {
                if (col.tag == Tags.GROUND_TAG)
                {
                    PickUPTxT.gameObject.SetActive(true);
                    player.GetComponent<Player>().canPickUPOBJ = true;
                }
            }
        }
        */
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.name == "EnemyRig")
        {
            Debug.LogError("CROWBAR");
            enemy.GetComponent<EnemyGreen>().TakeDMG(damage);
        }

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
