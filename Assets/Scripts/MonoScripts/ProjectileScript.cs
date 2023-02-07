using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
	private List<TroopScript> enemies;
	
	private int damage;
    
    public void SetValues(ref List<TroopScript> projectileEnemies, Vector3 projectileDirection, float projectileSpeed, float projectileLifetime, int projectileDamage)
    {
		enemies = projectileEnemies;
        Rigidbody2D projectileRb = GetComponent<Rigidbody2D>();
		projectileRb.velocity = projectileSpeed * projectileDirection;
		Object.Destroy(gameObject, projectileLifetime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
		for (int i = 0; i < enemies.Count; i++)
        {
			if (col.gameObject == enemies[i].gameObject)
			{
				Health enemyHealth = enemies[i].GetComponent<Health>();
				enemyHealth.Damage(damage);
				Object.Destroy(gameObject);
			}
		}
    }
}