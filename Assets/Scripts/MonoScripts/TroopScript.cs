using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopScript : UnitScript
{
    [SerializeField]
    protected Rigidbody2D rb;
	public Health health;
	protected bool troopIsDead;
	protected float speed;

	public bool isDead()
    {
        return troopIsDead;
    }

    public override void OnUpdate()
    {
		TroopScript target = GetClosestEnemy ();
		Vector2 toTarget = target.gameObject.transform.position - transform.position;
		if (toTarget.magnitude <= range && canFire)
		{
			StartCoroutine(FireProjectile(toTarget.normalized));
		}
		
        if (toTarget.magnitude > range - 0.05f)
		{
			rb.MovePosition(rb.position + toTarget.normalized * speed * Time.fixedDeltaTime);
		}
        if (toTarget.magnitude < range - 0.1f)
		{
			rb.MovePosition(rb.position - toTarget.normalized * speed * Time.fixedDeltaTime);
		}
    }
	
	private void Awake ()
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
