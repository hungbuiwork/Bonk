using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTroops : Shop
{
    [SerializeField]
    private Transform cursor;
    public override bool PurchaseCurrent(Inventory inventory)
    {
        bool result = base.PurchaseCurrent(inventory);
        if (result)
        {
            Instantiate(GetCurrent().Content, cursor.position, Quaternion.identity);
        }
        return result;
    }
}
