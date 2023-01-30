using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using System;

public class Inventory : MonoBehaviour
{
    /// <summary>
    /// Inventory script, used to manage inventory of (currently just "Gold").
    /// </summary>
    ///
 
    [SerializeField]
    private List<CurrencyType> InventoryKeys;
    [SerializeField]
    private List<int> InventoryValues;

    private Dictionary<CurrencyType, int> currencies = new Dictionary<CurrencyType, int>();

    public Action<CurrencyType, int> onResouceChanged;

    public void Awake()
    {
        for (int i = 0; i < InventoryKeys.Count; i++)
        {
            currencies.Add(InventoryKeys[i], InventoryValues[i]);
        }
    }
    public Dictionary<CurrencyType, int> GetInventory()
    {
        return currencies;
    }
    private void Refresh()
    {
        //REMOVE BEFORE BUILDING. Just serializes
        InventoryKeys.Clear();
        InventoryValues.Clear();
        foreach(var (key, value) in currencies)
        {
            InventoryKeys.Add(key);
            InventoryValues.Add(value);
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
        Refresh(); //DELETE LATER
    }

    public void RemoveResource(CurrencyType type, int quantity)
    {
        //Removes resource to inventory
        if (!currencies.ContainsKey(type)) { return; }
        currencies[type] = Mathf.Max( currencies[type] - quantity, 0);
        onResouceChanged(type, currencies[type]);
        Refresh();//DELETE LATER
    }

    public bool HasResource(CurrencyType type, int quantityToCheck)
    {
        ///Returns bool whether the quantity of that resource is there

        if (!currencies.ContainsKey(type)) { return false; }
        return currencies[type] >= quantityToCheck;
    }
}
