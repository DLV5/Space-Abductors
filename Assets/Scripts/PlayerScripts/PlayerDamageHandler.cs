using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerHealthUI))]
public class PlayerDamageHandler : MonoBehaviour, IDamageable
{
    private PlayerHealth _playerHealth;

    public bool IsInvincible { get; set; } = false;

    public event Action Damaged;
    public event Action InvinvibilityDisabled;

    private void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        Damaged += EnableInvincibility;
    }

    private void OnDestroy()
    {
        Damaged -= EnableInvincibility;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("EnemyBullet"))
        {
            Damage(1);
            collision.gameObject.SetActive(false);
        }
        //if (collision.CompareTag("HealingBullet"))
        //{
        //    Damage(-1);
        //    collision.gameObject.SetActive(false);
        //}
    }

    public void Damage(int damage)
    {
        if (IsInvincible)
            return;

        _playerHealth.Health -= damage;

        Damaged?.Invoke();
      
        if (_playerHealth.Health <= 0)
        {
            Die();
        }
    }

    private void EnableInvincibility()
    {
        IsInvincible = true;
        StartCoroutine(DisableInvincibilityAfterTime(1f));
    }

    private IEnumerator DisableInvincibilityAfterTime(float invincibilityDuration)
    {
        yield return new WaitForSeconds(invincibilityDuration);
        IsInvincible = false;
        InvinvibilityDisabled?.Invoke();
    }

    private void Die()
    {
        UIManager.Instance.OpenMenu(UIManager.Instance.DeathScreen);
        gameObject.SetActive(false);
    }
}
