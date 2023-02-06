using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public RoundController round;

    [SerializeField] GameObject shopDisplay;
    [SerializeField] GameObject inventoryDisplay;

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
        if(round.isStandbyPhase || round.isBattlePhase)
        {
            setPlayerUI(false);
        }

        if(round.isPrepPhase || round.isPrepPhase2)
        {
            setPlayerUI(true);
        }


    }

    void setPlayerUI(bool temp)
    {
        shopDisplay.SetActive(temp);
        inventoryDisplay.SetActive(temp);
    }
}
