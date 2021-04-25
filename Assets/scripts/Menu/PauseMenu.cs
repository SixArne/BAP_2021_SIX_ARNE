using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused;
    public GameObject pauseMenuUI;
    public AudioManager audioManager;

    public InputHandler inputHandler;

    private bool hasPausedLastFrame;

    // Update is called once per frame
    void Update()
    {
        if (InputHandler.HasPausedThisFrame && !hasPausedLastFrame)
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        hasPausedLastFrame = InputHandler.HasPausedThisFrame;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        IsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScreen");
    }

    public void LoadTutorial()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scenes/IntroScreen");
    }

    public void QuitGame()
    {
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;

        Application.Quit();
    }
}
