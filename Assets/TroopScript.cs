using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopScript : MonoBehaviour
{
	public List<TroopScript> enemies;
	float speed=1f;
	float range=1f;
	public LayerMask layerOfTargets;
	
	Transform GetClosestEnemy ()
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }

	public void UpdateEnemies (ref List<TroopScript> newEnemies)
	{
		enemies = newEnemies;
	}

    public void OnUpdate()
    {
		Transform target = GetClosestEnemy ();
		Vector3 toTarget = target.position - transform.position;
        if (toTarget.magnitude > range)
            rb.velocity = toTarget.normalized * speed;
        else if (toTarget.magnitude < range)
            rb.velocity = -toTarget.normalized * speed;
		
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
