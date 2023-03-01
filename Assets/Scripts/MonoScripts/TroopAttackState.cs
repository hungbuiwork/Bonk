using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopAttackState : State
{
    public override void UpdateLogic()
	{
		TroopScript target = ts.GetClosestEnemy();
		if (target != null)
		{
			Vector2 toTarget = target.gameObject.transform.position - ts.transform.position;
			ts.rb.velocity = Vector3.zero;
			if (ts.canFire)
			{
				ts.StartCoroutine(ts.FireProjectile(toTarget.normalized));
			}
			
			if (toTarget.magnitude > ts.unitStats.projectileRange)
			{
				sm.ChangeState(gameObject.AddComponent<TroopChaseState>());
			}
			if (ts.health < ts.GetMaxHealth() / 4)
			{
				sm.ChangeState(gameObject.AddComponent<TroopFleeState>());
			}
		}
	}
}
