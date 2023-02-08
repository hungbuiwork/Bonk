using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopScript : UnitScript
{
    protected Rigidbody2D rb;
	private float health;
	public delegate void OnDeath(float health);
	public OnDeath onDeath;
	public delegate void OnReceiveDamage(float health);
	public OnReceiveDamage onReceiveDamage;
	public delegate void OnReceiveHealth(float health);
	public OnReceiveHealth onReceiveHealth;
	protected bool troopIsDead;
	private float tooFar = -0.05f;
	private float tooClose = 0.1f;

	public bool isDead()
    {
        return troopIsDead;
    }

    public override void OnUpdate()
    {
		TroopScript target = GetClosestEnemy();
		if (target != null)
		{
			Vector2 toTarget = target.gameObject.transform.position - transform.position;
			if (toTarget.magnitude <= unitStats.projectileRange && canFire)
			{
				StartCoroutine(FireProjectile(toTarget.normalized));
			}
			
			if (toTarget.magnitude > unitStats.projectileRange + tooFar)
			{
				rb.MovePosition(rb.position + toTarget.normalized * unitStats.speed * Time.fixedDeltaTime);
			}
			if (toTarget.magnitude < unitStats.projectileRange - tooClose)
			{
				rb.MovePosition(rb.position - toTarget.normalized * unitStats.speed * Time.fixedDeltaTime);
			}
		}
    }
	
	private void Awake ()
	{
		rb = GetComponent<Rigidbody2D>();
		
		spriteRenderer.sprite = unitStats.aliveSprite;
		troopIsDead = false;
		health = unitStats.maxHealth;
		canFire = true;
	}
	
	public void Damage(int amount)
    {
        health -= amount;
        if (health < 0)
        {
            health = 0;
			Die();
        }
        if (onReceiveDamage != null) onReceiveDamage(health);
    }
	
	public void Heal(int amount)
    {
        health += amount;
        if (health > unitStats.maxHealth)
        {
            health = unitStats.maxHealth;
        }
        if (onReceiveDamage != null) onReceiveHealth(health);
    }
	
    private void Die()
    {
		spriteRenderer.sprite = unitStats.deadSprite;
		troopIsDead = true;
        //Emit a death delegate
        if (onDeath != null)
        {
            onDeath(health); 
        }
    }
	
	public float GetMaxHealth()
	{
		return unitStats.maxHealth;
	}
}
