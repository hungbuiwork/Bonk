using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UIElements;

public class HealerScript : TroopScript
{
    // Start is called before the first frame update

    protected override void Awake()
    {
        base.Awake();
        sm.ChangeState(gameObject.AddComponent<HealerPositionState>());
    }
    public Vector3 GetHealPosition()
    {
        //Calculates the positionto stand when healing
        //Find the average position of nearby allies. A nearby ally is within a radius of the unit's heal range * 2
        int nearbyAllies = 0;
        Vector3 positions = Vector3.zero;
        Vector3 closestAlly = Vector3.zero;
        foreach(TroopScript troop in allies) { 
            if (troop == this)
            {
                continue;
            }
            if(Vector3.Distance(troop.transform.position, this.transform.position) < unitStats.projectileRange * 2)
            {
                nearbyAllies++;
                positions += troop.transform.position;
                if ((this.transform.position - troop.transform.position).magnitude < (this.transform.position - closestAlly).magnitude)
                {
                    closestAlly = troop.transform.position;
                }
            }
        }
        if (nearbyAllies == 0) { return closestAlly; }
        
        Vector3 averagePosition = positions / nearbyAllies;
        //Slightly offset the position to be a little away from the closest source of danger
        Vector3 nearestDanger = GetClosestEnemy().transform.position;
        averagePosition += (averagePosition - nearestDanger).normalized * unitStats.projectileRange * 1 / 4;
        return averagePosition;
    }

    public override IEnumerator UseMain(Vector3 direction)
    {
        canFire = false;
        GameObject projectile = Instantiate(unitStats.projectilePrefab, this.transform.position, Quaternion.identity);
        projectile.transform.parent = this.transform;
        projectile.transform.localScale = Vector3.one * unitStats.projectileRange;
        foreach(TroopScript troop in allies)
        {
            if (Vector3.Distance(troop.transform.position, this.transform.position) < unitStats.projectileRange)
            {
                troop.Heal(unitStats.projectileDamage);
            }
        }
        /*
        GameObject projectile = Instantiate(unitStats.projectilePrefab, transform.position, Quaternion.identity);
        ProjectileScript projectileScript = projectile.GetComponent<ProjectileScript>();
        float projectileLifetime = unitStats.projectileRange / unitStats.projectileSpeed;
        projectileScript.SetValues(ref enemies, direction, unitStats.projectileSpeed, projectileLifetime, unitStats.projectileDamage);
        */

        yield return new WaitForSeconds(unitStats.projectileRate);
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
