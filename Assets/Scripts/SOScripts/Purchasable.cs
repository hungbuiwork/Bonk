using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

[CreateAssetMenu(menuName = "ICS167/Purchasable")]
public class Purchasable : ScriptableObject
{
    ///UNTESTED. (remove this line when tested)
    /// <summary>
    /// Base class for purchasables
    /// </summary>
    [SerializeField]
    private string Name;
    [SerializeField]
    private int Cost;
    [SerializeField]
    private string Description;
    [SerializeField]
    private Sprite Icon;

    public bool CanPurchase(Inventory inventory)
    {
        ///Compares a player's resources with this cost, returning a bool if the player has enough resources
        return inventory.HasResource(Cost);
    }
    
    public virtual bool Purchase(Inventory inventory)
    {
        ///Returns a bool depending on whether the transaction was possible
        ///Removes resources from the inventory passed in
        if (!CanPurchase(inventory))
        {
            return false;
        }
        inventory.RemoveResource(Cost);
        return true;
    }
}
