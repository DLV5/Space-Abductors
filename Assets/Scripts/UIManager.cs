using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject deathScreen;
    public GameObject pauseMenu;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (deathScreen == null)
        {
            deathScreen = GameObject.Find("DeathScreen");
        }
        if (pauseMenu == null)
        {
            pauseMenu = GameObject.Find("PauseMenu");
        }
    }

    // Common functions for UI buttons
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
