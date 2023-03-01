// UnitScript.cs
// By Cais Wang
// AI for troops in flee state

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopFleeState : State
{
	// Move away from closest enemy. If health exceeds max/4, transition to attack state
    public override void UpdateLogic()
	{
		TroopScript target = ts.GetClosestEnemy();
		if (target != null)
		{
			Vector2 toTarget = target.gameObject.transform.position - ts.transform.position;
			ts.rb.velocity = -toTarget.normalized * ts.unitStats.speed;
			if (ts.health > ts.GetMaxHealth() / 4)
			{
				sm.ChangeState(gameObject.AddComponent<TroopAttackState>());
			}
		}
	}
}
