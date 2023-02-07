using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopScript : MonoBehaviour
{
	public List<TroopScript> enemies;
	private float speed=1f;
	private float range=1f;
	private Rigidbody2D rb;
	
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

	public void UpdateEnemies (ref List<TroopScript> newEnemies)
	{
		enemies = newEnemies;
	}

    public void OnUpdate()
    {
		rb.velocity = new Vector3(0,0,0);
		
		TroopScript target = GetClosestEnemy ();
		Vector3 toTarget = target.gameObject.transform.position - transform.position;
        if (toTarget.magnitude > range)
            rb.velocity = toTarget.normalized * speed;
        else if (toTarget.magnitude < range)
            rb.velocity = -toTarget.normalized * speed;
    }
}
