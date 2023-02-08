using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ICS167/CurrencyType")]
public class CurrencyType : ScriptableObject
{
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
