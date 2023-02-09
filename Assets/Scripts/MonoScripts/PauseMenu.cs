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

}
