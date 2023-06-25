using UnityEngine;

public class SkillpointMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.CurrentPlayerState == GameManager.PlayerState.Paused)
        {
            CloseSkillpointMenu();
        }
    }

    private void CloseSkillpointMenu()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.CurrentPlayerState = GameManager.PlayerState.Playing;
        gameObject.SetActive(false);
    }
}
