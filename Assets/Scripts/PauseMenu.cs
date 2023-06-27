using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.CurrentState == GameManager.State.Paused) 
        {
            Continue();
        }
    }

    public void Continue()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.SetState(GameManager.State.Playing);
        //StartCoroutine(Weapon.Instance.EnterCooldown());
        gameObject.SetActive(false);
    }
}
