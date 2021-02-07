using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public Player player;
    public EnemyGreen enemyGreen;

    private void Update()
    {
        if(enemyGreen.GetCurrentHealth() <= 0 )
        {
            StartCoroutine(enemyGreen.Die());
        }
    }


}
