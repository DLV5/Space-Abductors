using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Continue()
    {
        gameObject.SetActive(false);
        GameManager.Instance.playerState = GameManager.PlayerState.Playing;
    }
}
