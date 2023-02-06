using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public RoundController round;

    [Header("Player 1 Stuff")]
    [SerializeField] GameObject troopShop;
    [SerializeField] GameObject cursor;

    [SerializeField] GameObject shopDisplay;
    [SerializeField] GameObject inventoryDisplay;

    [Header("Player 2 Stuff")]
    [SerializeField] GameObject troopShop2;
    [SerializeField] GameObject cursor2;

    [SerializeField] GameObject shopDisplay2;
    [SerializeField] GameObject inventoryDisplay2;

    List<GameObject> p1_objects = new List<GameObject>();
    List<GameObject> p2_objects = new List<GameObject>();

    // start
    void Start()
    {
        p1_objects.Add(troopShop);
        p1_objects.Add(cursor);
        p1_objects.Add(shopDisplay);
        p1_objects.Add(inventoryDisplay);

        p2_objects.Add(troopShop2);
        p2_objects.Add(cursor2);
        p2_objects.Add(shopDisplay2);
        p2_objects.Add(inventoryDisplay2);

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

    // game stuff
    
    void Update()
    {
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

    }

    void setPlayerUI(List<GameObject> player_objects, bool isActive)
    {
        
        for(int i = 0; i < player_objects.Count; i++)
        {
            player_objects[i].SetActive(isActive);
        }

        // i can't get the foreach loop to work, it keep saying i'm missing curly bracket error
        /*
        foreach(var object in player_objects)
        {
            object.SetActive(isActive);
        }*/
        
    }
}
