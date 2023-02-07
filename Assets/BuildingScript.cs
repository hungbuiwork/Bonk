using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
	public GameObject projectilePrefab;
	
	private List<TroopScript> enemies;
	private bool canFire;
	
	private float range = 1f; // Will be implemented better
	private float projectileRate = 0.5f;
	private float projectileSpeed = 4f;
	private float projectileLifetime = 1f;
	private int projectileDamage = 1;
	
    public void UpdateEnemies(ref List<TroopScript> newEnemies)
    {
        enemies.Clear();
        enemies = newEnemies;
    }

    public void OnUpdate()
    {
		TroopScript target = GetClosestEnemy ();
		Vector3 toTarget = target.gameObject.transform.position - transform.position;
		
        if (toTarget.magnitude <= range && canFire)
		{
			StartCoroutine(FireProjectile(toTarget.normalized));
		}
    }
	
	TroopScript GetClosestEnemy ()
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
	
	IEnumerator FireProjectile(Vector3 direction)
    {
		canFire = false;
		
        GameObject projectile = Instantiate(projectilePrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
		ProjectileScript projectileScript = projectile.GetComponent<ProjectileScript>();
		projectileScript.SetValues(ref enemies, direction, projectileSpeed, projectileLifetime, projectileDamage);
		
		yield return new WaitForSeconds(projectileRate);
		canFire = true;
    }
}
