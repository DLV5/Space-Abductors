using System.Collections;
using System.Collections.Generic;
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
    public PlayerState playerState;

    GameManager() { 
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            playerState = PlayerState.Paused;
            UIManager.instance.pauseMenu.SetActive(true);
        }
    }
}
