using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected int health;
    public int Health { get => health; set => health = value; }
    [SerializeField]
    protected int fireRate;
    public int FireRate { get => health; set => health = value; }


    [SerializeField]
    protected GameObject bulletPrefab;

    public void Damage(int damage)
    {
        Health -= damage;
        if (Health <= 0) 
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Damage(1);
            collision.gameObject.SetActive(false);
        }
    }
}
