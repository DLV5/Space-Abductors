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
    }

    private void Start()
    {
        Time.timeScale = 1;
        SetState(GameState.Playing);
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
                Pause.Instance.EnterPause();
                break;
            case GameState.Playing:
                Pause.Instance.ExitPause();
                break;
            case GameState.Finished:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
}
