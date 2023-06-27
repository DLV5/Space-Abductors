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
public class Skills : MonoBehaviour
{
    public static Skills Instance { get; set; }
    public List<Skill> SkillList { get; set; } = new List<Skill>();
    [SerializeField] private int _skillPoints = 0;
    public int SkillPoints
    {
        get => _skillPoints;
        set => _skillPoints = value;
    }
    [SerializeField] private TextMeshProUGUI _skillPointMenuText;

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
        _skillPointMenuText.text = SkillPoints + " skill points";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && GameManager.Instance.CurrentState == GameManager.State.Playing)
        {
            OpenSkillpointMenu();
        }
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

    public void OpenSkillpointMenu()
    {
        Time.timeScale = 0;
        GameManager.Instance.SetState(GameManager.State.Paused);
        UIManager.Instance.SkillpointMenu.SetActive(true);
    }

    public void AddSkillpoints(int pointsToAdd)
    {
        SkillPoints += pointsToAdd;
        _skillPointMenuText.text = SkillPoints + " skill points";
    }

    public void RefreshSkills()
    {
        //var instance = Weapon.Instance;
        //foreach (var skill in SkillList)
        //{
        //    switch (skill) // Add a string here for every new weapon skill
        //    {
        //        case Skill.Shotgun:
        //            instance.CurrentWeaponAttack = instance.ShootLikeShootgun;
        //            instance.Source.clip = instance.ShotgunSound;
        //            instance.Railgun.SetActive(false);
        //            instance.Flamethrower.SetActive(false);
        //            instance.CurrentType = Weapon.Type.ShootingWeapon;
        //            instance.SpreadAngle = 90f;
        //            instance.Damage = 1;
        //            break;
        //        case Skill.Railgun:
        //            instance.CurrentWeaponAttack = instance.ShootLikeRailgun;
        //            instance.Source.clip = instance.RailgunShotSound;
        //            instance.Flamethrower.SetActive(false);
        //            instance.RailgunHolder.SetActive(true);
        //            instance.CurrentType = Weapon.Type.ChargingWeapon;
        //            instance.Damage = 1;
        //            break;
        //        case Skill.Flamethrower:
        //            instance.CurrentWeaponAttack = instance.ShootLikeFlamethrower;
        //            instance.Source.clip = instance.FlamethrowerSound;
        //            instance.Source.loop = true;
        //            instance.Railgun.SetActive(false);
        //            instance.Flamethrower.SetActive(true);
        //            instance.CurrentType = Weapon.Type.HoldingWeapon;
        //            instance.Damage = 1;
        //            break;
        //        case Skill.ShotgunSpreadUpgrade:
        //            break;
        //        case Skill.ShotgunNumberUpgrade:
        //            break;
        //        case Skill.ShotgunDamageUpgrade:
        //            break;
        //        case Skill.ShotgunCooldownUpgrade:
        //            break;
        //        default: break;
        //    }
        //}
    }
}
