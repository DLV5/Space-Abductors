using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum State
    {
        Paused,
        Playing,
        Finished,
    }
    public State CurrentState;

    GameManager() { 
        Instance = this;
    }

    private void Awake()
    {
        Time.timeScale = 1;
        CurrentState = State.Playing;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && CurrentState == State.Playing) 
        {
            Time.timeScale = 0;
            CurrentState = State.Paused;
            UIManager.Instance.PauseMenu.SetActive(true);
        }
    }
}
