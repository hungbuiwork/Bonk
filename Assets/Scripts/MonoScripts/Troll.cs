using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : TroopScript
{
	private void Start() // Will be a scriptable object
	{
		speed = 1f;
		range = 1f;
		projectileRate = 0.5f;
		projectileSpeed = 4f;
		projectileLifetime = 1f;
		projectileDamage = 1;
	}
}
