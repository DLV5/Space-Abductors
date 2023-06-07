using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIActivator : MonoBehaviour
{
    [SerializeField]
    TMP_Text skillCostText;
    
    [SerializeField]
    Button skillBuyButton;

    [SerializeField] 
    List<Button> nextBranchesTreeToActivate;

    [SerializeField]
    GameObject boughtSkillImage;

    SkillParameter skillParameter;

    private void Start()
    {
        skillParameter = GetComponent<SkillParameter>();
    }
    public void ActivateChoosenGameObject(GameObject go)
    {
        go.SetActive(!go.activeSelf);
        UpdateSkillCostText();
        CanBuySkill();
    }

    private void UpdateSkillCostText() {
        skillCostText.text = skillParameter.price + " Skill Points";
    }

    private void CanBuySkill()
    {
        skillBuyButton.interactable = Skills.Instance.skillPoints >= skillParameter.price;
    }

    public void EnableImage()
    {
        boughtSkillImage.SetActive(true);

        foreach (Button go in nextBranchesTreeToActivate)
        {
            go.interactable = true;
        }
    }
}
