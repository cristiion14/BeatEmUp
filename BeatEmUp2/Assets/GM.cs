using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
public class GM : MonoBehaviour
{
    //instance
    public static GM instance { get; private set; }

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

    //side walls
    public GameObject sideWall;
    
    public bool isFighting = false;
   public bool hasFinishedFight = false;
   public bool hasInstantiatedWalls = false;
    List<GameObject> instantiatedSideWals = new List<GameObject>();
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
   //     SpawnEnemy(enemyPrefab, 1);
    }

    private void Update()
    {
        if (isFighting && !hasInstantiatedWalls)
        {
            //turn on side walls
           instantiatedSideWals.Add(Instantiate(sideWall, new Vector3(playerGB.transform.position.x - 5f, 2, 0), Quaternion.identity));
           instantiatedSideWals.Add(Instantiate(sideWall, new Vector3(playerGB.transform.position.x + 5f, 2, 0), Quaternion.identity));

            hasFinishedFight = false;
            hasInstantiatedWalls = true;
        }

        if(hasFinishedFight && hasInstantiatedWalls)
        {
            foreach (GameObject wall in instantiatedSideWals)
            {
                Destroy(wall);
            }
            hasInstantiatedWalls = false;
            hasFinishedFight = false;
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
