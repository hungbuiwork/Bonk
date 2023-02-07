using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitScript : MonoBehaviour
{
	[SerializeField] private GameObject projectilePrefab;
	
	protected List<TroopScript> enemies;
	protected bool canFire;
	
	protected float range;
	protected float projectileRate;
	protected float projectileSpeed;
	protected float projectileLifetime;
	protected int projectileDamage;
	
    public void UpdateEnemies(ref List<TroopScript> newEnemies)
    {
        enemies.Clear();
        enemies = newEnemies;
    }

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
	
	protected IEnumerator FireProjectile(Vector3 direction)
    {
		canFire = false;
		
        GameObject projectile = Instantiate(projectilePrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
		ProjectileScript projectileScript = projectile.GetComponent<ProjectileScript>();
		projectileScript.SetValues(ref enemies, direction, projectileSpeed, projectileLifetime, projectileDamage);
		
		yield return new WaitForSeconds(projectileRate);
		canFire = true;
    }

    public abstract void OnUpdate();
}
