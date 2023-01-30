using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Shop : MonoBehaviour
{
    /// </summary>
    /// Base class which contains a list of purchasables. Holds the logic
    /// For scrolling different purchasables and purchasing them
    /// </summary>

    [SerializeField]
    private List<Purchasable> items;
    [SerializeField]
    private int currentIndex = 0;
    [SerializeField]
    private Inventory inventory; //The current inventory being traded with

    public Action onPurchaseSuccess, onPurchaseFail, onPrev, onNext;

    public void Awake()
    {
        onPurchaseSuccess += successCallback;
        onPurchaseFail += failureCallback;
    }
    public void Update()
    {
        //DELETE LATER
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Previous();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Next();
        }
        if (Input.GetKeyDown(KeyCode.R)){
            PurchaseCurrent(inventory);
        }
    }
    public void Next()
    {
        //Changes current index to the next one
        currentIndex = (items.Count + currentIndex + 1) % items.Count;
        onNext();
    }
    public void Previous()
    {
        //Changes current index to the previous one
        currentIndex = (items.Count + currentIndex - 1 ) % items.Count;
        //adding items.count ensures we dont run into any negative numbers
        onPrev();
    }
    public bool canPurchaseCurrent(Inventory inventory)
    {
        Purchasable currentPurchasable = GetCurrent();
        if (inventory.GetAmount(currentPurchasable.CurrencyName) >= currentPurchasable.CostAmount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public virtual bool PurchaseCurrent(Inventory inventory)
    {
        ///Attempts to purchase the current item. Returns bool representing success of purchase.
        ///THIS METHOD SHOULD BE OVERRIDEN IN CHILD CLASS
        bool result = canPurchaseCurrent(inventory);
        if (result)
        {
            inventory.RemoveResource(GetCurrent().CurrencyName, GetCurrent().CostAmount);
            if (onPurchaseSuccess != null) { onPurchaseSuccess(); }
        }
        else
        {
            if (onPurchaseFail!= null) { onPurchaseFail(); }
        }
        return result;
    }
    
    public Purchasable GetInIndex(int index)
    {
        if (index < 0 || index >= items.Count)
        {
            return null;
        }
        return items[index];
    }
    public Purchasable GetCurrent()
    {
        ///Gets currently selected purchasable
        return items[currentIndex];
    }

    public Purchasable GetNext()
    {
        ///Gets purchasable to the right of current selected
        return items[(items.Count + currentIndex + 1) % items.Count];
    }
    public Purchasable GetPrevious()
    {
        ///Gets purchasable to the left of current selected
        return items[(items.Count + currentIndex - 1) % items.Count];
    }

    private void successCallback()
    {
        //DELETE LATER
        Debug.Log("SUCCESS");
    }
    private void failureCallback()
    {
        //DELETE LATER
        Debug.Log("FAILURE");
    }
}
