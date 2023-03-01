using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopFleeState : State
{
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
