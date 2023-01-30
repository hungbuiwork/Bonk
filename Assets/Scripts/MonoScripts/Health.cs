using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    public float CurrHealth { get; set; }
    [SerializeField]
    public float MaxHealth { get; private set; } = 100;
   
    public event Action<float> onDeath, onReceiveDamage, onReceiveHealth; //Delegates. Float is the current health

    private bool DeathAlreadyEmitted = false;

    void Awake()
    {
        CurrHealth = MaxHealth;
    }

    private void Update()
    {
        if (CurrHealth <= 0){
            CurrHealth = 0;
            if (!DeathAlreadyEmitted) { 
                DeathAlreadyEmitted = true;
                Die();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //DELETE LATER: FOR TESTING PURPOSES ONLY
            Damage(10);
        }
    }
    public void Damage(int amount)
    {
        //Damage by the amount and emit damage delegate
        CurrHealth -= amount;
        if (CurrHealth < 0)
        {
            CurrHealth= 0;
        }
        if (onReceiveDamage != null) onReceiveDamage(CurrHealth);
    }

    public void Heal(int amount)
    {
        //Heal the amount and emit heal delegate
        CurrHealth += amount;
        if (CurrHealth > MaxHealth)
        {
            CurrHealth = MaxHealth;
        }
        if (onReceiveDamage != null) onReceiveHealth(CurrHealth);
    }
    private void Die()
    {
        //Emit a death delegate
        if (onDeath != null)
        {
            onDeath(CurrHealth); 
        }
    }
}
