using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopScript : UnitScript
{
    [SerializeField]
    protected Rigidbody2D rb;
	public float health { get; set; }
	public float maxHealth { get; private set; }
	public delegate void OnDeath(float health);
	public OnDeath onDeath;
	public delegate void OnReceiveDamage(float health);
	public OnReceiveDamage onReceiveDamage;
	public delegate void OnReceiveHealth(float health);
	public OnReceiveHealth onReceiveHealth;
	protected bool troopIsDead;
	protected float speed;
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
			if (toTarget.magnitude <= range && canFire)
			{
				StartCoroutine(FireProjectile(toTarget.normalized));
			}
			
			if (toTarget.magnitude > range + tooFar)
			{
				rb.MovePosition(rb.position + toTarget.normalized * speed * Time.fixedDeltaTime);
			}
			if (toTarget.magnitude < range - tooClose)
			{
				rb.MovePosition(rb.position - toTarget.normalized * speed * Time.fixedDeltaTime);
			}
		}
    }
	
	private void Awake ()
	{
		rb = GetComponent<Rigidbody2D>();
		
		troopIsDead = false;
		health = maxHealth;
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
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (onReceiveDamage != null) onReceiveHealth(health);
    }
	
    private void Die()
    {
		troopIsDead = true;
        //Emit a death delegate
        if (onDeath != null)
        {
            onDeath(health); 
        }
    }
}
