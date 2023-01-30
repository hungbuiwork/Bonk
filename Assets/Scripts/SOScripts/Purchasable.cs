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
    public string CurrencyName;
    [SerializeField]
    public int CostAmount;

    //Content
    [Header("Content")]
    [SerializeField]
    public GameObject Content;



}
