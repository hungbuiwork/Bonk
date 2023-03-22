// UnitScript.cs
// By Cais Wang
// Class for AI state machines

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
	private TroopScript ts;
	private State state;
	
	// Update logic of current state
    public void UpdateState()
    {
        if (state != null)
		{
			state.UpdateLogic();
		}
    }
	
	// Change state object to new state
	public void ChangeState(State newState)
    {
		Destroy(state);
        state = newState;
		state.Enter(this, ts);
    }
	
	// Set troop script state machine interacts with
	public void SetTroopScript(TroopScript newTS)
	{
		ts = newTS;
	}
}
