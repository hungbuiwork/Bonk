using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Will change implementation for two players

public class InputEventManager : MonoBehaviour
{
	public delegate void OnMoveCursorEvent();
	public static OnMoveCursorEvent onMoveCursorEvent;
	public delegate void OnPlaceEvent();
	public static OnPlaceEvent onPlaceEvent;
	public delegate void OnEndTurnEvent();
	public static OnEndTurnEvent onEndTurnEvent;
	public delegate void OnPreviousTypeEvent();
	public static OnPreviousTypeEvent onPreviousTypeEvent;
	public delegate void OnNextTypeEvent();
	public static OnNextTypeEvent onNextTypeEvent;

    void OnMoveCursor()
    {
        onMoveCursorEvent?.Invoke();
    }
	void OnPlace()
    {
        onPlaceEvent?.Invoke();
    }
	void OnEndTurn()
    {
        onEndTurnEvent?.Invoke();
    }
	void OnPreviousType()
    {
        onPreviousTypeEvent?.Invoke();
    }
	void OnNextType()
    {
        onNextTypeEvent?.Invoke();
    }
}
