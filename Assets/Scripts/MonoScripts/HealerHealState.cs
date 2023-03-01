using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerHealState : State
{
    public override void UpdateLogic()
    {
        //calculate new position to walk to
        Vector3 newPosition = ((HealerScript)ts).GetHealPosition();
        Vector2 toTarget = newPosition - ts.transform.position;
        ts.rb.velocity = toTarget.normalized * ts.unitStats.speed;
        if (ts.canFire)
        {
            ts.StartCoroutine(ts.UseMain(toTarget.normalized));
        }

        if (toTarget.magnitude > ts.unitStats.projectileRange)
        {
            sm.ChangeState(gameObject.AddComponent<HealerPositionState>());
        }
        if (ts.health < ts.GetMaxHealth() / 4)
        {
            sm.ChangeState(gameObject.AddComponent<TroopFleeState>());
        }
    }
}
