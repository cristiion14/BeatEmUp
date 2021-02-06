﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    //health values
    private float initialHealth = 100, currentHealth, maxHealth;

    float shield = 100f;

    //Getters and Setters for the health
    public float GetCurrentHealth() { return currentHealth; }
    public void SetCurrentHealth(float newHealth) { currentHealth = initialHealth; }
    public float GetCurrentShield() { return shield; }
    public void SetCurrentShield(float newShield) { shield = newShield; }

    //UI
    public Image healthBar;
    public Image shieldBar;

    public Transform initialPos;
    // Start is called before the first frame update

    private void Awake()
    {
        //Initialize player
        PlayerInitialize();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthAndShield();

        Debug.Log(currentHealth);
    }

    void PlayerInitialize()
    {
        //Spawn player to initial position
        transform.position = initialPos.position;

        //Set Health
        currentHealth = initialHealth;

        //Assign health and armour sprite fill amount to the player's health
        healthBar.fillAmount = currentHealth / 100;

        shieldBar.fillAmount = shield / 100;

    }

    void UpdateHealthAndShield()
    {
        //Update the sprite fill amount
        healthBar.fillAmount = currentHealth / 100;
        shieldBar.fillAmount = shield / 100;
    }

    public void TakeDMG(float amount)
    {
        //take damage first from the shield
        if (shield > 0)
            shield -= amount;
       

        if (shield <= 0 && currentHealth > 0)
            currentHealth -= amount;

    }
}
