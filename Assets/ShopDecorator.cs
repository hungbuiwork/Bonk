using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopDecorator : MonoBehaviour
{
    [SerializeField] private Shop shop;

    [SerializeField] private Image currentImage;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI cost;
    
    [SerializeField] private Image prevImage;
    [SerializeField] private Image nextImage;

    private void Start()
    {
        Refresh();
        shop.onNext += Refresh;
        shop.onPrev += Refresh;
    }
    private void Refresh()
    {
        currentImage.sprite = shop.GetCurrent().Icon;
        prevImage.sprite = shop.GetPrevious().Icon;
        nextImage.sprite = shop.GetNext().Icon;
        title.text = shop.GetCurrent().Name;
        description.text =  shop.GetCurrent().Description;
        cost.text = shop.GetCurrent().CurrencyName + " : " + shop.GetCurrent().CostAmount.ToString();
    }
}
