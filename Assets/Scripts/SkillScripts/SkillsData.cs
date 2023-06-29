using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillsData", menuName = "ScriptableObjects/SkillsSkriptableObject", order = 1)]
public class SkillsData : ScriptableObject
{
    public GameObject CurrentWeapon { get; set; }
    [SerializeField] public TextMeshProUGUI SkillPointMenuText;
    [SerializeField] public GameObject PistolWeaponPrefab;
    [SerializeField] public GameObject ShotgunWeaponPrefab;
    [SerializeField] public GameObject RailgunWeaponPrefab;
    [SerializeField] public GameObject FlamethrowerWeaponPrefab;
}
