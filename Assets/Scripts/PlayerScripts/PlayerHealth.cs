using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(PlayerDamageHandler))]
[RequireComponent(typeof(PlayerHealthUI))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 3;

    private int _health;
    public int Health 
    {
        get => _health;
        set {
            if (_maxHealth <= value) // Handle value being larger than maxHealth
            {
                _health = _maxHealth;
                return;
            }
            if (value <= 0) // Handle value being 0 or less
            {
                _health = 0;
                return;
            }
            else
                _health = value;
        } 
    }

    private void Awake()
    {
        _health = _maxHealth;
    }
}
