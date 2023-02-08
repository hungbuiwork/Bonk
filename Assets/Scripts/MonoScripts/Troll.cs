using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : TroopScript
{
	private void Start() // Will be a scriptable object
	{
		range = 3f;
		projectileRate = 0.5f;
		projectileSpeed = 4f;
		projectileLifetime = 1f;
		projectileDamage = 3;
		
		speed = 1f;
	}
}
