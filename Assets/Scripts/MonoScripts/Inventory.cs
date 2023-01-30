using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public class Inventory : MonoBehaviour
{
    /// <summary>
    /// Inventory script, used to manage inventory of (currently just "Gold").
    /// </summary>
    ///
    [SerializeField]
    private int Gold; //TODO: Change to private set later

    public int getGold()
    {
        return Gold;
    }
    public void AddResource(int quantity)
    {
        ///Adds resource to inventory
        Gold += quantity;
    }

    public void RemoveResource(int quantity)
    {
        Debug.Log("REMOVING RESSOURCE");
        Gold -= quantity;
        if (Gold <= 0) {
            Gold = 0;
        }
    }

    public bool HasResource(int quantityToCheck)
    {
        ///Returns bool whether the quantity of that resource is there
        return quantityToCheck >= Gold;
    }
}
