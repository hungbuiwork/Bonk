using System;
using System.Collections;
using System.Collections.Generic;
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

    
    //UNCOMMENT THIS WHEN READY TO IMPLEMENT
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
    
    public void Update()
    {
        OnUpdate(); //temporarily calls here. In future will only be called when the battle round is active
    }
    public void OnUpdate()
    {
        CheckWinConditions();
        UpdateAllTroops();

        
    }

    private void UpdateAllTroops()
    {
        //Call update method for all troops
        UpdateTroops(ref aliveTroops1, ref deadTroops1, ref aliveTroops2);
        UpdateTroops(ref aliveTroops2, ref deadTroops2, ref aliveTroops2);
        UpdateBuildings(ref buildings1, ref aliveTroops2);
        UpdateBuildings(ref buildings2, ref aliveTroops1);
    }

    private void UpdateTroops(ref List<TroopScript> alive, ref List<TroopScript> dead, ref List<TroopScript> enemies)
    {
        //Given a reference to alive and dead lists, update each troop, moving it to the necessary dead list if needed.
        for (int i = alive.Count - 1; i >= 0; i--)
        {
            TroopScript troop = alive[i];
            if (troop.isDead()) //Create isDead for troopScript which determines if a troop is dead. Rename as needed
            {
                alive.RemoveAt(i);
                dead.Add(troop);
            }
            else
            {
                troop.UpdateEnemies(ref enemies);
                troop.OnUpdate(); //Calls OnUpdate for the respective troop.
                //OnUpdate should call movement, and attacking
            }
        }
    }

    private void UpdateBuildings(ref List<BuildingScript> buildings, ref List<TroopScript> enemies)
    {
        for (int i = buildings.Count - 1; i >= 0; i--)
        {
            BuildingScript building = buildings[i];
            building.UpdateEnemies(ref enemies);
            building.OnUpdate();
        }

    }
    private void CheckWinConditions()
    {
        //Current win conditions are complete death of one team.
        if (aliveTroops1.Count == 0)
        {
            if (onWin != null) { onWin(1); } //Team 1 won

        }
        else if (aliveTroops2.Count == 0){
            if (onWin != null) { onWin(2); } //Team 2 won
        }
    }

    private void CleanUpOnWin(int teamWon)
    {
        //for now, lets just clear all the lists when a game is won.
        //Will change later!
        aliveTroops1.Clear();
        aliveTroops2.Clear();
        deadTroops1.Clear();
        deadTroops2.Clear();
    }
    
}


