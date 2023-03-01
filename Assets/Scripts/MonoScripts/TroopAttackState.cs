// UnitScript.cs
// By Cais Wang
// AI for troops in attack state

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopAttackState : State
{
	// Attack closest enemy. If out of range, transition to chase state. If health drops below max/4, transition to flee state
    public override void UpdateLogic()
	{
		TroopScript target = ts.GetClosestEnemy();
		if (target != null)
		{
			Vector2 toTarget = target.gameObject.transform.position - ts.transform.position;
			ts.rb.velocity = Vector3.zero;
			if (ts.canFire)
			{
				ts.StartCoroutine(ts.UseMain(toTarget.normalized));
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
