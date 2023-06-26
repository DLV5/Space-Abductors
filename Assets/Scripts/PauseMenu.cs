using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Continue()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.SetState(GameManager.State.Playing);
        Weapon.Instance.CanShoot = true;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.CurrentState == GameManager.State.Paused) 
        {
            Continue();
        }
    }
}
