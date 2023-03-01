using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
	protected StateMachine sm;
	protected TroopScript ts;
	
	public void Enter(StateMachine newSM, TroopScript newTS)
	{
		sm = newSM;
		ts = newTS;
	}
	
	public abstract void UpdateLogic();
}
