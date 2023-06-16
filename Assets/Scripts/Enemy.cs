using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Attacker, IDamageable
{
    [SerializeField]
    protected int health;
    public int Health { get => health; set => health = value; }


    protected EnemyStates currentState = EnemyStates.FlyingToTheScreen;
    protected enum EnemyStates
    {
        FlyingToTheScreen,
        Attacking,
        Leaving
    }

    public void Damage(int damage)
    {
        Health -= damage;
        DamageUI.instance.ShowDamageOnEnemy(transform.position);
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
