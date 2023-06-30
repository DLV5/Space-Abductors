using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _skillsText;

    public void UpdateSkillPointsText()
    {
       _skillsText.text = Skills.Instance.SkillPoints + " skill points";
    }
}
