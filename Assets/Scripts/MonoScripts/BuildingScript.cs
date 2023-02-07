using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : UnitScript
{
    public override void OnUpdate()
    {
		TroopScript target = GetClosestEnemy();
		Vector3 toTarget = target.gameObject.transform.position - transform.position;
		
        if (toTarget.magnitude <= range && canFire)
		{
			StartCoroutine(FireProjectile(toTarget.normalized));
		}
    }
}
