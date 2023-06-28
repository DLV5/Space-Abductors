using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Paused,
    Playing,
    Finished
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public GameState CurrentState { get; private set;}

    [SerializeField] private GameState _currentState;

    private GameManager() { 
        Instance = this;
    }

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void Start()
    {
        SetState(GameState.Playing);
        InputHandler.PauseMenuButtonPressed += InputHandler_PauseMenuButtonPressed;
    }

    public void StartGame(string mode)
    {
        PlayerPrefs.SetString("Mode", mode);
        SceneManager.LoadScene(1);
    }

    public void SetState(GameState state)
    {
        switch (state)
        {
            case GameState.Paused:
                UIManager.Instance.OpenMenu(UIManager.Instance.PauseMenu);
                break;
            case GameState.Playing:
                UIManager.Instance.CloseMenu(UIManager.Instance.PauseMenu);
                break;
            case GameState.Finished:
                break;
            default:
                break;
        }
        CurrentState = state;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void InputHandler_PauseMenuButtonPressed()
    {
        switch (CurrentState)
        {
            case GameState.Playing:
                SetState(GameState.Paused);
                break;
            case GameState.Paused:
                SetState(GameState.Playing);
                break;
        }
    }
}
