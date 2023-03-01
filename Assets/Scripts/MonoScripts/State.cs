// UnitScript.cs
// By Cais Wang
// Base class for AI states

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
	protected StateMachine sm;
	protected TroopScript ts;
	
	// Enter a new state and set local objects
	public void Enter(StateMachine newSM, TroopScript newTS)
	{
		sm = newSM;
		ts = newTS;
	}
	
	// Abstract function for updating AI logic
	public abstract void UpdateLogic();
}
