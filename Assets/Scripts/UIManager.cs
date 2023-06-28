using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public GameObject DeathScreen;
    public GameObject PauseMenu;
    public GameObject SkillpointMenu;

    private UIManager()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Common functions for UI buttons
    public void ReturnToFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SwitchToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenMenu(GameObject menu)
    {
        Pause.Instance.EnterPause();
        menu.SetActive(true);
    }

    public void CloseMenu(GameObject menu)
    {
        Pause.Instance.ExitPause();
        menu.SetActive(false);
    }
}
