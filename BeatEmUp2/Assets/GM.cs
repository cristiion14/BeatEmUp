using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Cinemachine;
public class GM : MonoBehaviour
{
    #region Delegates Declaration and Functions
    //delegates
    public delegate void TestDelegate();
    public delegate bool BoolDelegate(int i);

    TestDelegate testDelegateFunc;
    BoolDelegate boolTestDelegate;

    void MyTestDelegateFunction()
    {
        Debug.LogError("Merge delegatul");
    }


    void secondFunc()
    {
        Debug.LogError($"second delegate cu valoare : {playerComboCounter}");
    }

    /// <summary>
    /// Return a random true or fals
    /// </summary>
    /// <returns></returns>
    bool RandomTrueOrFals(int x)
    {
        bool trueOrFalse =UnityEngine.Random.value > .5f;

        return trueOrFalse;

    }
    #endregion

    #region Built In Delegates
    //for void functions delegate use Action
    Action testAction;
    Action<int, float> testIntFloatAction;
    Action<bool, int, float> Action3Param;
   [SerializeField] Timer actionTimer;

    //for returning functions use delegate use FUNC
    Func<int, bool> FuncTest;
    Func<int, float> Func2ndTest;
    

    #endregion

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

        #region Delegates Testing With Lambda Expressions
        testDelegateFunc = MyTestDelegateFunction;

        testDelegateFunc();

        testDelegateFunc += secondFunc;

        testDelegateFunc();

        testDelegateFunc -= secondFunc;

        testDelegateFunc();

        boolTestDelegate = RandomTrueOrFals;
        Debug.LogError(boolTestDelegate(2));

        //another way of assigning delegate function
        testDelegateFunc = delegate () { Debug.LogError("2nd mode of test delegate func"); };
        testDelegateFunc();

        //3rd way using lambda expressions
        testDelegateFunc = () => { Debug.LogError("Lambda expressions"); };
        testDelegateFunc();


        //4th way using lambda expressions without brackets
        boolTestDelegate = (int i) => i<5;
        Debug.LogError( boolTestDelegate(3));

        //The problem with assigning delegates using the 2nd and 3rd attempt is:
        //that it cannot be assign to the previous one using += or -= operators due to lack of reference from the function


        #endregion

        #region Unity Built In Delegates Testing
        
        //void return built in delegate: Action

        //define action function
        testIntFloatAction = (int i, float f) =>
        {
            if (i > f)
                Debug.LogError("1");
            else
                Debug.LogError("2");

        };
        //call it
        testIntFloatAction(2, 4.2f);

        //define second action func
        Action3Param = (bool b, int i, float f) =>
        {
            if (b)
                Debug.LogError(i);
            else
                Debug.LogError(f);

        };
        //call it
        Action3Param(false, 100, 3.3f);

        //data type return built in delegate: Func

        //First Func definition
        FuncTest = (int i) =>
        {
           return i < 5;
        };

        //Call it
        FuncTest(4);

        //Second Func Def
        Func2ndTest = (int x) =>
        {
            return x - 2.3f;
            
        };

        //Call it
        Func2ndTest(5);

        #endregion

        #region Timer using Action 
        actionTimer.SetTimer(1f, () => { Debug.LogError("Timer Complete!"); });

        actionTimer.SetTimer(3f, () => { Debug.LogError("Delayed message"); });
        #endregion
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

        float offset =UnityEngine.Random.Range(-.4f, .4f);
        //instantiate enemies based on number parameter
    //    for (int i = 0; i < number; i++)
      //      enemyList.Add(Instantiate(enemy, new Vector3(enemy.GetComponent<EnemyGreen>().initialPos.transform.position.x +offset, enemy.GetComponent<EnemyGreen>().initialPos.transform.position.y, enemy.GetComponent<EnemyGreen>().initialPos.transform.position.z), Quaternion.identity));
          enemyList.Add(Instantiate(enemy, new Vector3(enemy.GetComponent<EnemyGreen>().initialPos.transform.position.x +offset, 
              enemy.GetComponent<EnemyGreen>().initialPos.transform.position.y,
              enemy.GetComponent<EnemyGreen>().initialPos.transform.position.z), Quaternion.identity));



    }


}
