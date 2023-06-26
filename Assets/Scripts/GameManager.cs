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

    public void SetState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Paused:
                Weapon.Instance.enabled = false;
                break;
            case PlayerState.Playing:
                Weapon.Instance.enabled = true;
                break;
            case PlayerState.Dead:
                break;
            default:
                break;
        }
        CurrentPlayerState = state;
    }

    private void Awake()
    {
        Time.timeScale = 1;
    }
    private void Start()
    {
        SetState(PlayerState.Playing);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && CurrentPlayerState == PlayerState.Playing) 
        {
            Time.timeScale = 0;
            SetState(PlayerState.Paused);
            Weapon.Instance.canShoot = false;
            UIManager.Instance.PauseMenu.SetActive(true);
        }
    }
}
