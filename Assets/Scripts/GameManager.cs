using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public State CurrentState;
    public enum State
    {
        Paused,
        Playing,
        Finished,
    }

    private GameManager() { 
        Instance = this;
    }

    public void SetState(State state)
    {
        switch (state)
        {
            case State.Paused:
                Weapon.Instance.enabled = false;
                break;
            case State.Playing:
                Weapon.Instance.enabled = true;
                break;
            case State.Finished:
                break;
            default:
                break;
        }
        CurrentState = state;
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
            Weapon.Instance.CanShoot = false;
            UIManager.Instance.PauseMenu.SetActive(true);
        }
    }
}
