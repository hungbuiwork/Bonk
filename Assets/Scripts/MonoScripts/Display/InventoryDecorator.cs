using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDecorator : MonoBehaviour
{
    /// <summary>
    /// Displays the inventory and the currencies inside, extending the inventory script.
    /// </summary>
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
        //Sets the UI up.
        foreach(var (currencyType, amount) in inventory.GetInventory())
        {
            InstantiateNewItem(currencyType, amount);
        }
    }

    private void InstantiateNewItem(CurrencyType currencyType, int amount)
    {
        //Instantiate a new item, changing its image and name to match the currency type and amount
        InventoryItemDisplay display = (InventoryItemDisplay) Instantiate(itemDisplayPrefab, targetTransform);
        display.transform.SetParent(targetTransform, false);
        display.Prime(currencyType, amount);
        itemDisplayList.Add(display);
    }
    private void RefreshItem(CurrencyType key, int newValue)
    {
        //Refresh the display of an item
        foreach (InventoryItemDisplay i in itemDisplayList)
        {
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
