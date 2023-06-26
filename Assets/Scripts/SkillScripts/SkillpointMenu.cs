using UnityEngine;

public class SkillpointMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.CurrentState == GameManager.State.Paused)
        {
            CloseSkillpointMenu();
        }
    }

    private void CloseSkillpointMenu()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.SetState(GameManager.State.Playing);
        gameObject.SetActive(false);
    }
}
