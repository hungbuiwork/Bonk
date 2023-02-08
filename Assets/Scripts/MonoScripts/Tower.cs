using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : BuildingScript
{
	private void Start() // Will be a scriptable object
	{
		range = 4f;
		projectileRate = 0.5f;
		projectileSpeed = 4f;
		projectileLifetime = 1f;
		projectileDamage = 3;
	}
}
