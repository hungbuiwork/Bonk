using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : UnitScript
{
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
		}
    }
	
	private void Awake ()
	{
		spriteRenderer.sprite = unitStats.aliveSprite;
		canFire = true;
	}
}
