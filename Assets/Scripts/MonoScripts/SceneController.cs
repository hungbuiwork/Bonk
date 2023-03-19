using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    /// <summary>
    /// Controlls the visibility of UI elements depending on what phase the game is in
    /// </summary>
    /// 
    [SerializeField]
    public RoundController round;

    [Header("Player 1 Stuff")]
    [SerializeField] private GameObject troopShop;
    [SerializeField] private GameObject cursor;

    [SerializeField] private GameObject shopDisplay;
    [SerializeField] private GameObject inventoryDisplay;

    [SerializeField] private GameObject p1_fog;

    [Header("Player 2 Stuff")]
    [SerializeField] private GameObject troopShop2;
    [SerializeField] private GameObject cursor2;

    [SerializeField] private GameObject shopDisplay2;
    [SerializeField] private GameObject inventoryDisplay2;

    [SerializeField] private GameObject p2_fog;

    

    List<GameObject> p1_objects = new List<GameObject>();
    List<GameObject> p2_objects = new List<GameObject>();

    // start
    void Start()
    {
        p1_objects.Add(troopShop);
        p1_objects.Add(cursor);
        p1_objects.Add(shopDisplay);
        p1_objects.Add(inventoryDisplay);
        p1_objects.Add(p1_fog);

        p2_objects.Add(troopShop2);
        p2_objects.Add(cursor2);
        p2_objects.Add(shopDisplay2);
        p2_objects.Add(inventoryDisplay2);
        p2_objects.Add(p2_fog);

    }

    // menu buttons

    public void QuitGame()
    {
        Debug.Log("QUIT_GAME");
        Application.Quit();
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void LoadScene(string title)
    {
        SceneManager.LoadScene(title);
    }


    // game stuff
    
    void Update()
    {
        //Sets the UI of the player depending on the round phase
        //TODO: In future, improve the design by using observer pattern to listen for phase changes so that we dont have to update every frame
        if(round.isPrepPhase)
        {
            setPlayerUI(p1_objects, true);

        }

        else if(round.isPrepPhase2)
        {
            setPlayerUI(p2_objects, true);
            setPlayerUI(p1_objects, false);
        }

        else if(round.isBattlePhase)
        {
            setPlayerUI(p2_objects, false);
        }

        //ensure it closes properly
        else if(round.isStandbyPhase)
        {
            setPlayerUI(p2_objects, false);
            setPlayerUI(p1_objects, false);
        }

    }

    void setPlayerUI(List<GameObject> player_objects, bool isActive)
    {
        
        for(int i = 0; i < player_objects.Count; i++)
        {
            player_objects[i].SetActive(isActive);
        }
        
    }
}
