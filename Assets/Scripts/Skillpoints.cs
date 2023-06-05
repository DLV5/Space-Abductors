using UnityEngine;
using TMPro;

public class Skillpoints : MonoBehaviour
{
    public static int skillPoints = 0;
    private Movement _movementScript; // For limiting movement
    private Cow _currentCow;
    private TextMeshProUGUI _skillPointMenuText;

    private void Start()
    {
        _skillPointMenuText = GameObject.Find("SkillpointText").GetComponent<TextMeshProUGUI>();
        _movementScript = GetComponent<Movement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && GameManager.Instance.playerState == GameManager.PlayerState.Playing)
        {
            OpenSkillpointMenu();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StealCow();
        }
        if (!_movementScript.canMove)
        {
            if (!_currentCow.moving)
            {
                _currentCow.gameObject.SetActive(false);
                AddSKillpoints(1);
                _movementScript.canMove = true;
            }
        }
    }

    private void StealCow()
    {
        if (!_movementScript.canMove) return;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        Collider2D cow = hit.collider;
        if (cow == null) return;
        if (cow.CompareTag("Cow"))
        {
            _currentCow = cow.gameObject.GetComponent<Cow>();
            _currentCow.moving = true;
            _movementScript.canMove = false;
        }
    }

    public void OpenSkillpointMenu()
    {
        Time.timeScale = 0;
        GameManager.Instance.playerState = GameManager.PlayerState.Paused;
        UIManager.instance.skillpointMenu.SetActive(true);
    }

    public void AddSKillpoints(int pointsToAdd)
    {
        skillPoints += pointsToAdd;
        _skillPointMenuText.text = skillPoints + " skill points";
    }
}
