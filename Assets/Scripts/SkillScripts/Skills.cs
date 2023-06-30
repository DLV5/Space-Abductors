using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Skill
{
    Shotgun,
    Railgun,
    Flamethrower,
    ShotgunSpreadUpgrade,
    ShotgunNumberUpgrade,
    ShotgunDamageUpgrade,
    ShotgunCooldownUpgrade
}

[RequireComponent(typeof(SkillsObjectsManager))]
[RequireComponent(typeof(SkillsUI))]
public class Skills : MonoBehaviour
{
    public static Skills Instance { get; set; }
    public List<Skill> SkillList { get; set; } = new List<Skill>();

    private SkillsUI _skillUI;

    [SerializeField] private int _skillPoints = 0;
    public int SkillPoints
    {
        get => _skillPoints;
        set => _skillPoints = value;
    }

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
        _skillUI = GetComponent<SkillsUI>();
        _skillUI.UpdateSkillPointsText();
    }

    public void BuySkill(SkillParameter parameter)
    {
        if (SkillPoints < parameter.Price) 
            return;
        parameter.IsBought = true;
        SkillList.Add(parameter.Name);
        AddSkillpoints(-parameter.Price);
        RefreshSkills();
    }

    public void AddSkillpoints(int pointsToAdd)
    {
        SkillPoints += pointsToAdd;
        _skillUI.UpdateSkillPointsText();
    }

    public void RefreshSkills()
    {
        var _instance = SkillsObjectsManager.Instance;
        foreach (var skill in SkillList)
        {
            switch (skill) // Add a string here for every new weapon skill
            {
                case Skill.Shotgun:
                    _instance.ChangeWeapon(_instance.Weapons["ShotgunWeapon"]);
                    break;
                case Skill.Railgun:
                    _instance.ChangeWeapon(_instance.Weapons["RailgunWeapon"]);
                    break;
                case Skill.Flamethrower:
                    _instance.ChangeWeapon(_instance.Weapons["FlamethrowerWeapon"]);
                    break;
                case Skill.ShotgunSpreadUpgrade:
                    break;
                case Skill.ShotgunNumberUpgrade:
                    break;
                case Skill.ShotgunDamageUpgrade:
                    break;
                case Skill.ShotgunCooldownUpgrade:
                    break;
                default: break;
            }
        }
    }
}
