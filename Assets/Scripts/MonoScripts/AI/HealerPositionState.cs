using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerPositionState : State
{
    /// <summary>
    /// FSM Healer Position State
    /// </summary>
    public override void UpdateLogic()
    {
        //calculate new position to walk to
        Vector3 newPosition = ((HealerScript)ts).GetHealPosition();
        Vector2 toTarget = newPosition - ts.transform.position;
        ts.rb.velocity = toTarget.normalized * ts.unitStats.speed;
        if (toTarget.magnitude < ts.unitStats.projectileRange)
        {
            sm.ChangeState(gameObject.AddComponent<HealerHealState>());
        }

    }
}
