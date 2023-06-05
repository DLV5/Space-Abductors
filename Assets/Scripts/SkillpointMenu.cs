using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillpointMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.playerState == GameManager.PlayerState.Paused)
        {
            CloseSkillpointMenu();
        }
    }

    private void CloseSkillpointMenu()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.playerState = GameManager.PlayerState.Playing;
        gameObject.SetActive(false);
    }
}
