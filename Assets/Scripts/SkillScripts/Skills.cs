using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public static Skills Instance;
    [HideInInspector] public List<string> SkillList = new List<string>();
    public int SkillPoints = 0;
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
        if (Input.GetKeyDown(KeyCode.L) && GameManager.Instance.CurrentPlayerState == GameManager.PlayerState.Playing)
        {
            OpenSkillpointMenu();
        }
    }

    public void BuySkill(SkillParameter parameter)
    {
        if (SkillPoints < parameter.Price) return;
        parameter.IsBought = true;
        SkillList.Add(parameter.SkillName);
        AddSkillpoints(-parameter.Price);
        RefreshSkills();
    }

    public void OpenSkillpointMenu()
    {
        Time.timeScale = 0;
        GameManager.Instance.SetState(GameManager.PlayerState.Paused);
        UIManager.Instance.SkillpointMenu.SetActive(true);
    }

    public void AddSkillpoints(int pointsToAdd)
    {
        SkillPoints += pointsToAdd;
        _skillPointMenuText.text = SkillPoints + " skill points";
    }

    public void RefreshSkills()
    {
        var instance = Weapon.Instance;
        foreach (string skill in SkillList)
        {
            switch (skill) // Add a string here for every new weapon skill
            {
                case "Shotgun":
                    instance.CurrentWeaponAttack = instance.ShootLikeShootgun;
                    instance.Railgun.SetActive(false);
                    instance.Flamethrower.SetActive(false);
                    instance.Type = Weapon.WeaponType.ShootingWeapon;
                    instance.SpreadAngle = 90f;
                    instance.Damage = 1;
                    break;
                case "Railgun":
                    instance.CurrentWeaponAttack = instance.ShootLikeRailgun;
                    instance.Source.clip = instance.RailgunShotSound;
                    instance.Flamethrower.SetActive(false);
                    instance.RailgunHolder.SetActive(true);
                    instance.Type = Weapon.WeaponType.ChargingWeapon;
                    instance.Damage = 1;
                    break;
                case "Flamethrower":
                    instance.CurrentWeaponAttack = instance.ShootLikeFlamethrower;
                    instance.Source.clip = instance.FlamethrowerSound;
                    instance.Source.loop = true;
                    instance.Railgun.SetActive(false);
                    instance.Flamethrower.SetActive(true);
                    instance.Type = Weapon.WeaponType.HoldingWeapon;
                    instance.Damage = 1;
                    break;
                case "ShotgunSpreadUpgrade":
                    instance.SpreadAngle = 40f;
                    break;
                case "ShotgunNumberUpgrade":
                    instance.BulletsPerShotgunShot = 10;
                    break;
                case "ShotgunDamageUpgrade":
                    instance.Damage = 2;
                    break;
                case "ShotgunCooldownUpgrade":
                    instance.Cooldown = 0.5f;
                    break;
                default: break;
            }
        }
    }
}
