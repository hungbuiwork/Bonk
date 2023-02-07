using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopScript : UnitScript
{
	protected Rigidbody2D rb;
	protected Health health;
	protected bool troopIsDead;
	protected float speed;

	public bool isDead()
    {
        return troopIsDead;
    }

    public override void OnUpdate()
    {
		rb.velocity = new Vector3(0,0,0);
		
		TroopScript target = GetClosestEnemy ();
		Vector3 toTarget = target.gameObject.transform.position - transform.position;
		
        if (toTarget.magnitude > range)
		{
			rb.velocity = toTarget.normalized * speed;
		}
        else
		{
			if (toTarget.magnitude < range)
			{
				rb.velocity = -toTarget.normalized * speed;
			}
			if (canFire)
			{
				StartCoroutine(FireProjectile(toTarget.normalized));
			}
		}
    }
	
	private void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		health = GetComponent<Health>();
		health.onDeath += Kill;
		
		troopIsDead = false;
		canFire = true;
	}
	
	private void Kill (float health)
	{
		troopIsDead = true;
	}
}
