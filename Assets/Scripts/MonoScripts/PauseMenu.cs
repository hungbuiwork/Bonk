using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// Pause menu implementation.
    /// </summary>
    [SerializeField]
    private SceneController sceneController;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject warningMessage;

    [SerializeField] private GameObject p1_instruct;
    [SerializeField] private GameObject p2_instruct;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
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
