using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using System;

public class Inventory : MonoBehaviour
{
    /// <summary>
    /// Inventory script, used to manage inventory of different currency types.
    /// </summary>
    ///

    [SerializeField]
    private List<CurrencyType> InventoryKeys;
    [SerializeField]
    private List<int> InventoryValues;
    [SerializeField]
    private List<CurrencyType> currencyPerRoundKeys; //delete later
    [SerializeField]
    private List<int> currencyPerRoundValues; //delete later

    private Dictionary<CurrencyType, int> currencies = new Dictionary<CurrencyType, int>();

    public Action<CurrencyType, int> onResouceChanged;

    public void Awake()
    {
        //Initialize the Dictionary with values from inspector
        for (int i = 0; i < InventoryKeys.Count; i++)
        {
            currencies.Add(InventoryKeys[i], InventoryValues[i]);
        }
        //make a copy, so each round we can reset the inventory
        currencyPerRoundKeys = new List<CurrencyType>(InventoryKeys);
        currencyPerRoundValues= new List<int>(InventoryValues);

        //Find the game controller, and observe when the standby phase begins, to reset currency
        GameObject.FindObjectOfType<RoundController>().beginStandby += resetCurrencyPerRound; 
    }

    public void resetCurrencyPerRound() 
    {
        //Reset the currency each round
        currencies.Clear();
        for (int i = 0; i < currencyPerRoundKeys.Count; i++)
        {
            currencies.Add(currencyPerRoundKeys[i], currencyPerRoundValues[i]);
        }
        Refresh();
        if (currencyPerRoundValues.Count >= 2) { currencyPerRoundValues[1] += 5; }
        //every round, give everyone 5 extra blood essence.

    }
    public Dictionary<CurrencyType, int> GetInventory()
    {
        return currencies;
    }
    private void Refresh()
    {
        //This just refreshes the view in the inspector(for some reason dictionaries cannot be viewed in inspector)
        InventoryKeys.Clear();
        InventoryValues.Clear();
        foreach(var (key, value) in currencies)
        {
            InventoryKeys.Add(key);
            InventoryValues.Add(value);
            onResouceChanged(key, value);
        }

    }
    public int GetAmount(CurrencyType type)
    {
        if (currencies.ContainsKey(type))
        {
            return currencies[type];
        }
        return 0;
    }
    public void AddResource(CurrencyType type, int quantity)
    {
        ///Adds resource to inventory
        
        if (currencies.ContainsKey(type))
        {
            currencies[type] += quantity;
        }
        else
        {
            currencies.Add(type, quantity);
        }
        onResouceChanged(type, currencies[type]);
        Refresh();
    }

    public void RemoveResource(CurrencyType type, int quantity)
    {
        //Removes resource to inventory
        if (!currencies.ContainsKey(type)) { return; }
        currencies[type] = Mathf.Max( currencies[type] - quantity, 0);
        onResouceChanged(type, currencies[type]);
        Refresh();
    }

    public bool HasResource(CurrencyType type, int quantityToCheck)
    {
        ///Returns bool whether the quantity of that resource is there
        if (!currencies.ContainsKey(type)) { return false; }
        return currencies[type] >= quantityToCheck;
    }
}
