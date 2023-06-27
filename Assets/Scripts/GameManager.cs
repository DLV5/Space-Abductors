using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public State CurrentState { get; private set;}

    [SerializeField] private State _currentState;

    public enum State
    {
        Paused,
        Playing,
        Finished,
    }

    private GameManager() { 
        Instance = this;
    }

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void Start()
    {
        SetState(State.Playing);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && CurrentState == State.Playing) 
        {
            Time.timeScale = 0;
            SetState(State.Paused);
            UIManager.Instance.PauseMenu.SetActive(true);
        }
    }

    public void SetState(State state)
    {
        switch (state)
        {
            case State.Paused:
                break;
            case State.Playing:
                break;
            case State.Finished:
                break;
            default:
                break;
        }
        CurrentState = state;
    }
}
