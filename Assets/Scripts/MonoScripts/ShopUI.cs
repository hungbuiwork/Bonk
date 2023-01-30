using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    /// </summary>
    /// Base class which contains a list of 
    /// </summary>

    public List<Purchasable> items;
    [SerializeField]
    private int currentIndex = 0;
    public void Next()
    {
        //Changes current index to the next one
        currentIndex = (currentIndex + 1) % items.Count;
    }
    public void Previous()
    {
        //Changes current index to the previous one
        currentIndex = (items.Count + currentIndex - 1 ) % items.Count;
        //adding items.count ensures we dont run into any negative numbers
    }
    public virtual bool PurchaseCurrent(Inventory inventory)
    {
        ///Attempts to purchase the current item. Returns bool representing success of purchase
        bool result = items[currentIndex].Purchase(inventory);
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
}
