using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public static Skills Instance;
    public List<string> skillList = new List<string>();
    public int skillPoints = 0;
    [SerializeField]
    private TextMeshProUGUI _skillPointMenuText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _skillPointMenuText.text = skillPoints + " skill points";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && GameManager.Instance.playerState == GameManager.PlayerState.Playing)
        {
            OpenSkillpointMenu();
        }
    }

    public void AddSkill(string skillName)
    {
        skillList.Add(skillName);
    }

    public void OpenSkillpointMenu()
    {
        Time.timeScale = 0;
        GameManager.Instance.playerState = GameManager.PlayerState.Paused;
        UIManager.instance.skillpointMenu.SetActive(true);
    }

    public void AddSkillpoints(int pointsToAdd)
    {
        skillPoints += pointsToAdd;
        _skillPointMenuText.text = skillPoints + " skill points";
    }
}
