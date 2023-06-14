using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamage : MonoBehaviour
{
    [SerializeField]
    private Weapon _playerWeapon;

    private void Start()
    {
        if (_playerWeapon == null)
            _playerWeapon = GameObject.Find("Player").GetComponent<Weapon>();
    }

    public void AddDamage(int damageToAdd)
    {
        _playerWeapon.damage += damageToAdd;
    }
}
