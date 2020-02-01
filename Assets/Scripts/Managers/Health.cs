using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float setHealth;
    private float currentHealth;

    public event Action OnDeath;

    void Start()
    {
        currentHealth = setHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        CheckDeath();
    }

    void CheckDeath()
    {
        if(OnDeath != null && currentHealth <= 0)
        {
            OnDeath();
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void ResetHealth()
    {
        currentHealth = setHealth;
    }
}
