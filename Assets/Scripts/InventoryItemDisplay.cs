using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDisplay : MonoBehaviour
{
    /// <summary>
    /// Displays an item in the inventory, changing the icon and text to match the currency type.
    /// </summary>
    [SerializeField]
    public CurrencyType currency;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TextMeshProUGUI text;

    public void Prime(CurrencyType currencyType, int amount)
    {
        if (icon != null) { icon.sprite = currencyType.getIcon(); }
        if (text != null) { text.text = amount.ToString(); }
        currency = currencyType;

    }


}
