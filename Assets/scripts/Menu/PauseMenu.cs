using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused;
    public GameObject pauseMenuUI;
    public AudioManager audioManager;

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Escape))
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
        */
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        IsPaused = false;
        
        if (audioManager != null) audioManager.Resume();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        IsPaused = true;
        
        if (audioManager != null) audioManager.Pause();
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
        Application.Quit();
    }
}
