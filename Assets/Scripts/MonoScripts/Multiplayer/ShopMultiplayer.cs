using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShopMultiplayer : Shop
{
    /// <summary>
    /// This is the online multiplayer implementation of the shop. This synchronizes
    /// the troop spawning in an online multiplayer game.
    /// </summary>
    [SerializeField]
    private Transform cursor;
    [SerializeField]
    private int team; //1 or 2, depending on which team

    [SerializeField]
    private UnitManager unitManager;
	
	private PhotonView photonView;

    private void Awake()
    {
        if (unitManager == null)
        {
            Debug.LogError("You need to assign a unit manager to this shop script");
        }
		photonView = PhotonView.Get(this);
    }
	
	public override bool PurchaseCurrent(Inventory inventory)
    {
        //Purchase the currently selected item
        bool result = base.PurchaseCurrent(inventory);
        if (result)
        {
            photonView.RPC("AddUnit", RpcTarget.All, GetIndex(), cursor.position);
        }
        return result;
    }
	
	[PunRPC]
    private void AddUnit(int index, Vector3 position)
	{
		//Instantiate object
		GameObject instance = Instantiate(GetInIndex(index).Content, position, Quaternion.identity);
		//Add Object to the Unit Manager(to manage the units for the round)
		TroopScript troop = instance.GetComponent<TroopScript>();
		BuildingScript building = instance.GetComponent<BuildingScript>();
		if (troop != null)
		{
			unitManager.AddTroop(team, troop);
		}
		if (building != null)
		{
			unitManager.AddBuilding(team, building);
		}
	}
}
