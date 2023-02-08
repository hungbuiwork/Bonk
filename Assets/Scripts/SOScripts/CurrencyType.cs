using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ICS167/CurrencyType")]
public class CurrencyType : ScriptableObject
{
    /// <summary>
    /// A currency type is an image and name related with a specific currency.
    /// </summary>
    [SerializeField]
    private string _name;
    [SerializeField]
    private Sprite icon;

    public string getName()
    {
        return _name;
    }

    public Sprite getIcon()
    {
        return icon;
    }
}
