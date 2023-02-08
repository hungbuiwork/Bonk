using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    /// <summary>
    /// This script manages 2 teams of troops. Calls each troops update scripts, and manages
    /// them when they die. Emits onWin action when one team loses all of its troops.
    /// </summary>


    //Singleton pattern. ONLY ONE should exist in the scene.
    public static UnitManager instance; 


    [SerializeField]
    private List<TroopScript> aliveTroops1 = new List<TroopScript>();
    [SerializeField]
    private List<TroopScript> aliveTroops2 = new List<TroopScript>();

    [SerializeField]
    private List<TroopScript> deadTroops1 = new List<TroopScript>();
    [SerializeField]
    private List<TroopScript> deadTroops2 = new List<TroopScript>();

    [SerializeField]
    private List<BuildingScript> buildings1;
    [SerializeField]
    private List<BuildingScript> buildings2;


    public Action<int> onWin; //1 for team 1 and 2 for team 2

  
    public void AddTroop(int team, TroopScript troop)
    {
        if (team == 1)
        {
			troop.UpdateEnemies(ref aliveTroops2);
            aliveTroops1.Add(troop);
        }
        else if (team == 2)
        {
			troop.UpdateEnemies(ref aliveTroops1);
            aliveTroops2.Add(troop);
        }
        else {
            Debug.LogError("Team must be either 1 or 2");
        }
    }
    public void AddBuilding(int team, BuildingScript building)
    {
        if (team == 1)
        {
			building.UpdateEnemies(ref aliveTroops2);
            buildings1.Add(building);
        }
        else if (team == 2)
        {
			building.UpdateEnemies(ref aliveTroops1);
            buildings2.Add(building);
        }
    }
    private void Awake()
    {
        //Singleton Pattern
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        //Clean Up function between rounds
        onWin += CleanUpOnWin;
    }
    
    public void OnUpdate()
    {
        Debug.Log("Calling OnUpdate");
        CheckWinConditions();
        UpdateAllTroops();

        
    }

    private void UpdateAllTroops()
    {
        //Call update method for all troops
        UpdateTroops(ref aliveTroops1, ref deadTroops1);
        UpdateTroops(ref aliveTroops2, ref deadTroops2);
        UpdateBuildings(ref buildings1);
        UpdateBuildings(ref buildings2);
    }

    private void UpdateTroops(ref List<TroopScript> alive, ref List<TroopScript> dead)
    {
        //Given a reference to alive and dead lists, update each troop, moving it to the necessary dead list if needed.
        for (int i = alive.Count - 1; i >= 0; i--)
        {
            TroopScript troop = alive[i];
            if (troop.isDead()) //Create isDead for troopScript which determines if a troop is dead. Rename as needed
            {
                Debug.Log("Removing troop");

                alive.RemoveAt(i);
                dead.Add(troop);
            }
            else
            {
                troop.OnUpdate(); //Calls OnUpdate for the respective troop.
                //OnUpdate should call movement, and attacking
            }
        }
    }

    private void UpdateBuildings(ref List<BuildingScript> buildings)
    {
        for (int i = buildings.Count - 1; i >= 0; i--)
        {
            BuildingScript building = buildings[i];
            building.OnUpdate();
        }

    }
    private void CheckWinConditions()
    {
        //Current win conditions are complete death of one team.
        if (aliveTroops1.Count == 0)
        {
            if (onWin != null) { onWin(2); } //Team 1 won

        }
        else if (aliveTroops2.Count == 0){
            if (onWin != null) { onWin(1); } //Team 2 won
        }
    }

    private void CleanUpOnWin(int teamWon)
    {
        //1) TODO: Play any animations, etc 
        //2) Clear the units on the board
        Invoke("DestroyAllUnits", 3);
    }

    private void DestroyAllUnits()
    {
  
        DestroyTroopList(ref aliveTroops1);
        DestroyTroopList(ref aliveTroops2);
        DestroyTroopList(ref deadTroops1);
        DestroyTroopList(ref deadTroops2);
        DestroyBuildingList(ref buildings1);
        DestroyBuildingList(ref buildings2);
    }

    private void DestroyTroopList(ref List<TroopScript> list)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            Destroy(list[i].gameObject); //IDK IF THIS IS CORRECTLY WORKING LMAO
        }
        list.Clear();
    }
    private void DestroyBuildingList(ref List<BuildingScript> list)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            Destroy(list[i].gameObject);
        }
        list.Clear();
    }
    
}


