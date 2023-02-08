using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

[CreateAssetMenu(fileName = "ICS167/Purchasable")]
public class Purchasable : ScriptableObject
{
    /// <summary>
    /// a Purchasable is the information, costs, and content that is used in the shop.
    /// </summary>

    //Item information
    [Header("Item Info")]
    [SerializeField]
    public string Name;
    [SerializeField]
    public string Description;
    [SerializeField]
    public Sprite Icon;

    //Sale info
    [Header("Cost Info")]
    [SerializeField]
    public CurrencyType CurrencyType;
    [SerializeField]
    public int CostAmount;

    //Content
    [Header("Content")]
    [SerializeField]
    public GameObject Content;



}
