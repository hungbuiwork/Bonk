using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTroops : Shop
{
    [SerializeField]
    private Transform cursor;
    [SerializeField]
    private int team; //1 or 2, depending on which team

    [SerializeField]
    private UnitManager unitManager;

    private void Awake()
    {
        if (unitManager == null)
        {
            Debug.LogError("You need to assign a unit manager to this shop script");
        }
    }
    public override bool PurchaseCurrent(Inventory inventory)
    {
        //First, make the purchase
        bool result = base.PurchaseCurrent(inventory);
        if (result)
        {
            //Instantiate object
            GameObject instance = Instantiate(GetCurrent().Content, cursor.position, Quaternion.identity);
            //Add Object to the Unit Manager(to manage the units for the round)
            TroopScript troop = instance.GetComponent<TroopScript>(); //PROBLEM: Troopscript 
            BuildingScript building = instance.GetComponent<BuildingScript>(); //PROBLEM: 
            Debug.Log(troop);
            Debug.Log(instance);
            if (troop != null)
            {
                unitManager.AddTroop(team, troop);
            }
            if (building != null)
            {
                unitManager.AddBuilding(team, building);
            }

        }
        return result;
    }
}
