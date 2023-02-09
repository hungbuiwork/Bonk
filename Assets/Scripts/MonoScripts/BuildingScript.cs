// BuildingScript.cs
// By Cais Wang
// Child class of units for buildings

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : UnitScript
{
	// If closest target is in range, fire at them
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
	
	// Reset variables and set sprite
	private void Awake ()
	{
		spriteRenderer.sprite = unitStats.aliveSprite;
		canFire = true;
	}
}
