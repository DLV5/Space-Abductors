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
        if (parameter.Type == SkillParameter.SkillType.Skill)
            RefreshSkills();
        else RefreshWeapon();
    }

    public void AddSkillpoints(int pointsToAdd)
    {
        SkillPoints += pointsToAdd;
        _skillUI.UpdateSkillPointsText();
    }

    public void RefreshWeapon()
    {
        var _instance = SkillsObjectsManager.Instance;
        foreach (var skill in SkillList)
        {
            switch (skill) // Add a string here for every new weapon
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
                default: break;
            }
        }
    }

    public void RefreshSkills()
    {
        var _instance = SkillsObjectsManager.Instance;
        foreach (var skill in SkillList)
        {
            switch (skill) // Add a string here for every new skill
            {
                case Skill.ShotgunSpreadUpgrade:
                    _instance.CurrentWeapon.GetComponent<ShotgunWeapon>().SpreadAngle = 30;
                    break;
                case Skill.ShotgunNumberUpgrade:
                    _instance.CurrentWeapon.GetComponent<ShotgunWeapon>().BulletsPerShotgunShot = 9;
                    break;
                case Skill.ShotgunDamageUpgrade:
                    _instance.CurrentWeapon.GetComponent<ShotgunWeapon>().Damage = 50;
                    break;
                case Skill.ShotgunCooldownUpgrade:
                    _instance.CurrentWeapon.GetComponent<ShotgunWeapon>().FireRate = 2;
                    break;
                default: break;
            }
        }
    }
}
