using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Continue()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.SetState(GameManager.PlayerState.Playing);
        Weapon.Instance.canShoot = true;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.CurrentPlayerState == GameManager.PlayerState.Paused) 
        {
            Continue();
        }
    }
}
