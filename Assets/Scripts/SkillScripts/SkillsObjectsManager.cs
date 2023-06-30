using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillsObjectsManager : MonoBehaviour 
{
    public static SkillsObjectsManager Instance { get; set; }

    public GameObject CurrentWeapon { get; set; }
    //[SerializeField] public TextMeshProUGUI SkillPointMenuText;
    
    public Dictionary<string, GameObject> Weapons { get; set; } = new Dictionary<string, GameObject>();

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
        Weapons = FindAllWeapons();
        CurrentWeapon = Weapons["PistolWeapon"];
    }


    public void ChangeWeapon(GameObject newWeapon)
    {
        CurrentWeapon.SetActive(false);
        CurrentWeapon = newWeapon;
        CurrentWeapon.SetActive(true);
    }

    //Return dictionary of GameObject with "Weapon" in name
    private Dictionary<string, GameObject> FindAllWeapons()
    {
        Dictionary<string, GameObject> weapons = new Dictionary<string, GameObject>();
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>(true);
        foreach (var gameObj in allGameObjects)
        {
            if (gameObj.name.Contains("Weapon")){
                weapons.Add(gameObj.name, gameObj);
            }
        }
        return weapons;
    }
}
