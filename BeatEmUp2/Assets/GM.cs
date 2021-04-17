using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
public class GM : MonoBehaviour
{
    public Player player;
    public EnemyGreen enemyGreen;

    public GameObject playerGB;
    public int enemyNR;
    List<GameObject> enemyList = new List<GameObject>();
    public GameObject enemyPrefab;
    public GameObject bodyCol, headCol, bodyP, headP;

    float timer = 20f;

   public bool canIncreaseCombo = false;
    public TextMeshProUGUI comboCounterTXT;
    float playerComboCounter = 0f;
    float playerComboTimer = 2f;

    private void Start()
    {
   //     SpawnEnemy(enemyPrefab, 1);
    }

    private void Update()
    {


  //      comboCounterTXT.text = playerGB

        /*
        Debug.Log(playerGB.GetComponentInChildren<AttackUniversal>().canIncreaseCombo);

        if (playerGB.GetComponentInChildren<AttackUniversal>().canIncreaseCombo)
            playerComboCounter++;

        if(!playerGB.GetComponentInChildren<PlayerAttack>().isPunching && !playerGB.GetComponentInChildren<PlayerAttack>().isKicking)
        {
            playerComboTimer -= Time.deltaTime;
            if (playerComboTimer <= 0)
            {
                player.GetComponentInChildren<AttackUniversal>().canIncreaseCombo = false;
                playerComboTimer = 2;
            }
        }

        */
        // Timer to spawn enemies

        /*

        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Debug.LogError("INSTANTIATED ENEMY");

            SpawnEnemy(enemyPrefab, enemyNR);
            timer += 20;
        }

        */


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
