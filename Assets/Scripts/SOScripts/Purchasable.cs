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
    
    //Sale/Item information
    [SerializeField]
    public string Name;
    [SerializeField]
    public int Cost;
    [SerializeField]
    public string Description;
    [SerializeField]
    public Sprite Icon;

    //Content
    [SerializeField]
    public GameObject Content;



}
