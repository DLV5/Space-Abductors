using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIActivator : MonoBehaviour
{
    private static GameObject[] _allIcons = new GameObject[] {};

    [SerializeField] private TMP_Text _skillCostText;
    
    [SerializeField] private Button _skillBuyButton;

    [SerializeField] private List<Button> _nextBranchesTreeToActivate;
    
    [SerializeField] private List<Button> _nextBranchesTreeToDeactivate;

    [SerializeField] private GameObject _boughtSkillImage;

    private SkillParameter _skillParameter;

    private GameObject _skillBox;

    private void Start()
    {
        _allIcons = GameObject.FindGameObjectsWithTag("SkillIcon");
        _skillParameter = GetComponent<SkillParameter>();
    }
    public void ActivateChoosenGameObject(GameObject go)
    {
        if (!go.activeSelf)
        {
            CloseAllIcons();
        } 
        go.SetActive(!go.activeSelf);
        UpdateSkillCostText();
        CheckCanBuySkill();
        _skillBox = go;
    }

    public void EnableImage()
    {
        _boughtSkillImage.SetActive(true);

        foreach (Button go in _nextBranchesTreeToActivate)
        {
            if (go != null)
            {
                go.interactable = true;
            }
        }
        foreach (Button go in _nextBranchesTreeToDeactivate)
        {
            if (go != null)
            {
                go.interactable = false;
            }
        }
        CloseAllIcons();
        _skillBox.SetActive(false);
    }

    private void UpdateSkillCostText() {
        _skillCostText.text = _skillParameter.Price + " Skill Points";
    }

    private void CheckCanBuySkill()
    {
        _skillBuyButton.interactable = Skills.Instance.SkillPoints >= _skillParameter.Price;
    }

    private void CloseAllIcons()
    {
        foreach (GameObject iconTextGameObject in _allIcons)
        {
            var child = iconTextGameObject.transform.GetChild(0).gameObject;
            if (child.activeSelf)
            { 
                child.SetActive(false);
            }
        }
    }
}
