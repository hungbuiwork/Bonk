// UnitScript.cs
// By Cais Wang
// Provides base functionality for troops and buildings

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitScript : MonoBehaviour
{
	// List of enemy troops for targeting
	protected List<TroopScript> enemies;
	protected bool canFire;
	[SerializeField]
	protected UnitStats unitStats;
	[SerializeField]
	protected SpriteRenderer spriteRenderer;
	
	// Set list of enemy troops after creation
    public void UpdateEnemies(ref List<TroopScript> newEnemies)
    {
        enemies = newEnemies;
    }

	// Return TroopScript of closest enemy
	protected TroopScript GetClosestEnemy()
    {
        TroopScript bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(TroopScript enemy in enemies)
        {
            Vector3 directionToTarget = enemy.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = enemy;
            }
        }
        return bestTarget;
    }
	
	// Instantiate a projectile and prevent unit from firing for a while
	protected IEnumerator FireProjectile(Vector3 direction)
    {
		canFire = false;
		
        GameObject projectile = Instantiate(unitStats.projectilePrefab, transform.position, Quaternion.identity);
		ProjectileScript projectileScript = projectile.GetComponent<ProjectileScript>();
		float projectileLifetime = unitStats.projectileRange / unitStats.projectileSpeed;
		projectileScript.SetValues(ref enemies, direction, unitStats.projectileSpeed, projectileLifetime, unitStats.projectileDamage);
		
		yield return new WaitForSeconds(unitStats.projectileRate);
		canFire = true;
    }

	// Provide abstract update function for main unit manager
    public abstract void OnUpdate();
}
