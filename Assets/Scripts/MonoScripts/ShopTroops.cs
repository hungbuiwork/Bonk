using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTroops : Shop
{
    [SerializeField]
    private Transform cursor;
    private int team; //0 or 1, depending on which team
    public override bool PurchaseCurrent(Inventory inventory)
    {
        bool result = base.PurchaseCurrent(inventory);
        if (result)
        {
            Instantiate(GetCurrent().Content, cursor.position, Quaternion.identity);
            //TODO: Initialize the team of the spawned game object. 
        }
        return result;
    }
}
