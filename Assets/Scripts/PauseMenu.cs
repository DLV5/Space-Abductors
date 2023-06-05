using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Continue()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.playerState = GameManager.PlayerState.Playing;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.playerState == GameManager.PlayerState.Paused) 
        {
            Continue();
        }
    }
}
