using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// Pause menu implementation, used for hiding/displayinig specific UI elements, and also freezes time.
    /// </summary>
    [SerializeField]
    private SceneController sceneController;

    [SerializeField] private RoundController roundController;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject warningMessage;

    [SerializeField] private GameObject p1_instruct;
    [SerializeField] private GameObject p2_instruct;
    private bool paused = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Awake()
    {
        roundController = FindObjectOfType<RoundController>();
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        //Don't stop time if online
        if (roundController.useOnlineRounds)
        {
            Time.timeScale = 1f;
        }
        paused= true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused= false;
    }

    public void Home()
    {
        warningMessage.SetActive(true);
    }

    public void YesToHome()
    {
        Time.timeScale = 1f;
        sceneController.PreviousScene();
    }

    public void NoToHome()
    {
        warningMessage.SetActive(false);
    }

    public void ToggleInstructions()
    {
        p1_instruct.SetActive(!p1_instruct.activeSelf);
        p2_instruct.SetActive(!p2_instruct.activeSelf);
    }

}
