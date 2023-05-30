using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ship : MonoBehaviour, IDamageable
{
    public int health;
    public int damage;

    [SerializeField]
    private TextMeshProUGUI _hpText;

    private void Start()
    {
        if (_hpText == null)
        {
            _hpText = GameObject.Find("HpText").GetComponent<TextMeshProUGUI>();
        }
        _hpText.text = "HP: " + health;
    }

    public void Damage(int damage)
    {
        health -= damage;
        _hpText.text = "HP: " + health;
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BaseBullet"))
        {
            Damage(1);
            collision.gameObject.SetActive(false);
        }
    }
}
