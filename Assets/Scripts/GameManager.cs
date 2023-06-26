using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum PlayerState
    {
        Paused,
        Playing,
        Dead,
    }
    public PlayerState CurrentPlayerState;

    GameManager() { 
        Instance = this;
    }

    private void Awake()
    {
        Time.timeScale = 1;
        CurrentPlayerState = PlayerState.Playing;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && CurrentPlayerState == PlayerState.Playing) 
        {
            Time.timeScale = 0;
            CurrentPlayerState = PlayerState.Paused;
            UIManager.Instance.PauseMenu.SetActive(true);
        }
    }
}
