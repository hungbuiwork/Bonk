// UnitScript.cs
// By Cais Wang
// AI for troops in chase state

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopChaseState : State
{
	// Move torwards closest enemy. If in range, transition to attack state.
    public override void UpdateLogic()
	{
		TroopScript target = ts.GetClosestEnemy();
		if (target != null)
		{
			Vector2 toTarget = target.gameObject.transform.position - ts.transform.position;
			ts.rb.velocity = toTarget.normalized * ts.unitStats.speed;
			if (toTarget.magnitude < ts.unitStats.projectileRange)
			{
				sm.ChangeState(gameObject.AddComponent<TroopAttackState>());
			}
		}
	}
}
