using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    // Start is called before the first frame update
    private List<TroopScript> _enemies; //rename as needed
    public void UpdateEnemies(ref List<TroopScript> enemies)
    {
        _enemies.Clear();
        _enemies = enemies;
    }

    public void OnUpdate()
    {
        //update
    }
}
