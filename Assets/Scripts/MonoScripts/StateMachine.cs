using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
	private TroopScript ts;
	private State state;
	
    public void UpdateState()
    {
        if (state != null)
		{
			state.UpdateLogic();
		}
    }
	
	public void ChangeState(State newState)
    {
		Destroy(state);
        state = newState;
		state.Enter(this, ts);
    }
	
	public void SetTroopScript(TroopScript newTS)
	{
		ts = newTS;
	}
}
