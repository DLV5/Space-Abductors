using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public GameObject DeathScreen;
    public GameObject PauseMenu;
    public GameObject SkillpointMenu;

    [SerializeField] private Texture2D _crosshair;

    private GameObject _currentMenu;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        if (SceneManager.GetActiveScene().buildIndex != 0)
            Cursor.SetCursor(_crosshair, new Vector2(_crosshair.width / 2, _crosshair.height / 2), CursorMode.Auto);
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
        _currentMenu = menu;
        GameManager.Instance.SetState(GameState.Paused);
        menu.SetActive(true);
    }

    public void OpenMenuWindow(GameObject window)
    {
        window.SetActive(true);
    }

    public void CloseMenuWindow(GameObject window)
    {
        window.SetActive(false);
    }

    public void CloseMenu(GameObject menu)
    {
        if (_currentMenu != menu)
            return;
        GameManager.Instance.SetState(GameState.Playing);
        menu.SetActive(false);
    }
}
