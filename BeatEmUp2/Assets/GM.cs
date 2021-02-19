using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public Player player;
    public EnemyGreen enemyGreen;

    public int enemyNR;
    List<GameObject> enemyList = new List<GameObject>();
    public GameObject enemyPrefab;

    float timer = 20f;


    private void Start()
    {
   //     SpawnEnemy(enemyPrefab, 1);
    }

    private void Update()
    {

        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Debug.LogError("INSTANTIATED ENEMY");

            SpawnEnemy(enemyPrefab, enemyNR);
            timer += 20;
        }
    }



    /// <summary>
    /// Function to spawn kinds of enemies
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    void SpawnEnemy(GameObject enemy, int number)
    {

        float offset = Random.Range(-.4f, .4f);
        //instantiate enemies based on number parameter
    //    for (int i = 0; i < number; i++)
      //      enemyList.Add(Instantiate(enemy, new Vector3(enemy.GetComponent<EnemyGreen>().initialPos.transform.position.x +offset, enemy.GetComponent<EnemyGreen>().initialPos.transform.position.y, enemy.GetComponent<EnemyGreen>().initialPos.transform.position.z), Quaternion.identity));
          enemyList.Add(Instantiate(enemy, new Vector3(enemy.GetComponent<EnemyGreen>().initialPos.transform.position.x +offset, 
              enemy.GetComponent<EnemyGreen>().initialPos.transform.position.y,
              enemy.GetComponent<EnemyGreen>().initialPos.transform.position.z), Quaternion.identity));



    }


}
