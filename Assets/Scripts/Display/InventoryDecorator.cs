using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDecorator : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private Transform targetTransform;
    [SerializeField]
    private InventoryItemDisplay itemDisplayPrefab;
    [SerializeField]
    private List<InventoryItemDisplay> itemDisplayList;

    private void Start()
    {
        Prime();
        inventory.onResouceChanged += RefreshItem;
    }
    public Inventory GetInventory() { return inventory; }
    public void Prime()
    {
        foreach(var (currencyType, amount) in inventory.GetInventory())
        {
            InstantiateNewItem(currencyType, amount);
        }
    }

    private void InstantiateNewItem(CurrencyType currencyType, int amount)
    {
        InventoryItemDisplay display = (InventoryItemDisplay) Instantiate(itemDisplayPrefab, targetTransform);
        display.transform.SetParent(targetTransform, false);
        display.Prime(currencyType, amount);
        itemDisplayList.Add(display);
    }
    private void RefreshItem(CurrencyType key, int newValue)
    {
        //BUGS
        Debug.Log(itemDisplayList.Count);
        foreach (InventoryItemDisplay i in itemDisplayList)
        {
            //For some reason, items inside of the list are NULL
            if (i.currency.name == key.name)
            {
                i.Prime(key, newValue);
                return;
            }
        }
        //If nothing was found, instantiate that new item
        InstantiateNewItem(key, newValue);
    }
}
