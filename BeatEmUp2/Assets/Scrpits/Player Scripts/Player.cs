using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    //health values
    private float initialHealth = 100f, currentHealth, maxHealth = 100f;

    float initialShield = 100f, currentShield, maxShield = 100f;

    public Transform initialPos;

    //Getters and Setters for the health
    public float GetCurrentHealth() { return currentHealth; }
    public void SetCurrentHealth(float newHealth) { currentHealth = initialHealth; }
    public float GetCurrentShield() { return currentShield; }
    public void SetCurrentShield(float newShield) { currentShield = newShield; }

    //UI
    public Image healthBar;
    public Image shieldBar;


    public GM gm;

    //FXs
    public GameObject playerDeathFX;
    public GameObject playerReviveFX;

    // Start is called before the first frame update

    private void Awake()
    {
        //Initialize player
        PlayerInitialize();
        gm = GameObject.Find("GM").GetComponent<GM>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthAndShield();


      currentHealth =  Mathf.Clamp(currentHealth, 0, maxHealth);
      currentShield =  Mathf.Clamp(currentShield, 0, maxShield);

        //activate/deactivate healing animation
        //heal player
        if (Input.GetKey(KeyCode.E))
        {
            GetComponentInChildren<CharacterAnimation>().Smoke(true);

                //Healing

            if (currentHealth >= 0 && currentShield <= 0 && currentHealth <= maxHealth)
                currentHealth += .25f;

            if (currentHealth >=maxHealth  && currentShield >= 0 && currentShield <= 100)
                currentShield += .25f;
        }

        if(Input.GetKeyDown(KeyCode.E))
            gm.GetComponent<AudioManager>().Play("Healing", true);


        if (Input.GetKeyUp(KeyCode.E))
        {
            GetComponentInChildren<CharacterAnimation>().Smoke(false);
            gm.GetComponent<AudioManager>().Stop("Healing");
        }

    }

    void PlayerInitialize()
    {
        //Spawn player to initial position
        transform.position = initialPos.position;

        //Set Health
        currentHealth = initialHealth;

        //set shield
        currentShield = initialShield;
    }

    public IEnumerator Die()
    {
        //play sound
        gm.GetComponent<AudioManager>().Play("Enemy Death", false);

        //instantiate death FX
        Instantiate(playerDeathFX, transform.position, Quaternion.identity);

        //deactivate the collider
        GetComponent<Collider>().enabled = false;

        GetComponentInChildren<PlayerAttack>().playerAnim.Death();
        yield return new WaitForSeconds(1.25f);

        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;

        yield return new WaitForSeconds(.5f);

        //instantiate revive fx
        Instantiate(playerReviveFX, initialPos);

        //set gb to active
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;


        PlayerInitialize();

    }

    void UpdateHealthAndShield()
    {
        //Update the sprite fill amount
        healthBar.fillAmount = currentHealth / 100;
        shieldBar.fillAmount = currentShield / 100;
    }

    public void TakeDMG(float amount)
    {
        //take damage first from the currentShield
        if (currentShield > 0)
            currentShield -= amount;
       

        if (currentShield <= 0 && currentHealth > 0)
            currentHealth -= amount;


        if (currentHealth <= 0)
            StartCoroutine(Die());


    }
}
