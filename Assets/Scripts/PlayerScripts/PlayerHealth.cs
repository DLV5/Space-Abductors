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
            if (_health - value > 0 && _health - value < _maxHealth)
            {
                _health = value;
            } 
        } 
    }

    private void Awake()
    {
        _health = _maxHealth;
    }
}
