// TroopScript.cs
// By Cais Wang
// Child class of units for troops

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopScript : UnitScript
{
    public Rigidbody2D rb;
	private StateMachine sm;
	public float health;
	// Delegates in case something happens when troop dies, etc.
	public delegate void OnDeath(float health);
	public OnDeath onDeath;
	public delegate void OnReceiveDamage(float health);
	public OnReceiveDamage onReceiveDamage;
	public delegate void OnReceiveHealth(float health);
	public OnReceiveHealth onReceiveHealth;
	protected bool troopIsDead;

	// Carry out current state's AI behavior
    public override void OnUpdate()
    {
		sm.UpdateState();
    }
	
	// Reset variables/objects and set sprite
	private void Awake ()
	{
		rb = GetComponent<Rigidbody2D>();
		
		spriteRenderer.sprite = unitStats.aliveSprite;
		troopIsDead = false;
		health = unitStats.maxHealth;
		canFire = true;
		sm = GetComponent<StateMachine>();
		sm.SetTroopScript(this);
		sm.ChangeState(gameObject.AddComponent<TroopChaseState>());
	}
	
	// Take damage and die if health drops below zero
	public void Damage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            health = 0;
			Die();
        }
        if (onReceiveDamage != null) onReceiveDamage(health);
    }
	
	// Heal health, maxing out at max health
	public void Heal(int amount)
    {
        health += amount;
        if (health > unitStats.maxHealth)
        {
            health = unitStats.maxHealth;
        }
        if (onReceiveDamage != null) onReceiveHealth(health);
    }
	
	// Set dead sprite and variable
    private void Die()
    {
		rb.velocity = Vector3.zero;
		spriteRenderer.sprite = unitStats.deadSprite;
		troopIsDead = true;
        // Emit a death delegate
        if (onDeath != null)
        {
            onDeath(health); 
        }
    }
	
	// Getter for max health
	public float GetMaxHealth()
	{
		return unitStats.maxHealth;
	}
	
	// Getter for troopIsDead
	public bool isDead()
    {
        return troopIsDead;
    }
}
