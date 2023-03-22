using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PopUpMenu : MonoBehaviour
{
    /// <summary>
    /// Very simple script that activates/deactivates a game object. To be used on a button when pressed, usually used for UI.
    /// </summary>
    public GameObject Menu;
    public bool isActive = false;

    public void Switch()
    {
        isActive = !isActive;
        Menu.SetActive(isActive);
    }
}
