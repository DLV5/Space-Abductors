using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIActivator : MonoBehaviour
{
    private static GameObject[] allIcons = new GameObject[] {};

    [SerializeField]
    private TMP_Text skillCostText;
    
    [SerializeField]
    private Button skillBuyButton;

    [SerializeField] 
    private List<Button> nextBranchesTreeToActivate;
    
    [SerializeField] 
    private List<Button> nextBranchesTreeToDeactivate;

    [SerializeField]
    private GameObject boughtSkillImage;

    private SkillParameter skillParameter;

    private GameObject skillBox;

    private void Start()
    {
        allIcons = GameObject.FindGameObjectsWithTag("SkillIcon");
        skillParameter = GetComponent<SkillParameter>();
    }
    public void ActivateChoosenGameObject(GameObject go)
    {
        if(!go.activeSelf) CloseAllIcons();
        go.SetActive(!go.activeSelf);
        UpdateSkillCostText();
        CanBuySkill();
        skillBox = go;
    }

    private void UpdateSkillCostText() {
        skillCostText.text = skillParameter.Price + " Skill Points";
    }

    private void CanBuySkill()
    {
        skillBuyButton.interactable = Skills.Instance.SkillPoints >= skillParameter.Price;
    }

    private void CloseAllIcons()
    {
        foreach (GameObject go in allIcons)
        {
            GameObject child = go.transform.GetChild(0).gameObject;
            if(child.activeSelf) child.SetActive(false);
        }
    }

    public void EnableImage()
    {
        boughtSkillImage.SetActive(true);

        foreach (Button go in nextBranchesTreeToActivate)
        {
            if (go != null)
                go.interactable = true;
        }
        foreach (Button go in nextBranchesTreeToDeactivate)
        {
            if (go != null)
                go.interactable = false;
        }
        CloseAllIcons();
        skillBox.SetActive(false);
    }
}
