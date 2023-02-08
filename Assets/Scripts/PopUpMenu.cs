using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PopUpMenu : MonoBehaviour
{
    public GameObject Menu;
    public bool isActive = false;

    public void Switch()
    {
        isActive = !isActive;
        Menu.SetActive(isActive);
    }
}
