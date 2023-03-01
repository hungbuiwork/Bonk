// ProjectileScript.cs
// By Cais Wang
// Manages projectile object

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
	// List of enemy troops for collision
	private List<TroopScript> enemies;
	// How much damage the projectile does
	private int damage;
 
	// Set projectile stat values after instantiation
    public void SetValues(ref List<TroopScript> projectileEnemies, Vector3 projectileDirection, float projectileSpeed, float projectileLifetime, int projectileDamage)
    {
		enemies = projectileEnemies;
        Rigidbody2D projectileRb = GetComponent<Rigidbody2D>();
		projectileRb.velocity = projectileSpeed * projectileDirection;
		Object.Destroy(gameObject, projectileLifetime);
		damage = projectileDamage;
    }

	// If projectile collides with an enemy, damage the enemy and destroy the projectile
    void OnTriggerEnter2D(Collider2D col)
    {
		for (int i = 0; i < enemies.Count; i++)
        {
			if (col.gameObject == enemies[i].gameObject)
			{
				enemies[i].Damage(damage);
				Object.Destroy(gameObject);
			}
		}
    }
}
