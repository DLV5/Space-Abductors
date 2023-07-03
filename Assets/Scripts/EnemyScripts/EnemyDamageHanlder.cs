using UnityEngine;

public class EnemyDamageHanlder : MonoBehaviour, IDamageable
{
    protected EnemyUI _enemyUI;

    [SerializeField] protected int _health;
    public int Health
    {
        get => _health;
        set => _health = value;
    }

    private static FlamethrowerWeapon _flamethrowerWeapon;

    protected virtual void Awake()
    {
        _enemyUI = GetComponent<EnemyUI>();
        _flamethrowerWeapon = FindObjectOfType<FlamethrowerWeapon>(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("PlayerBullet"))
        {
            HandleBulletDamage(collision.tag);
            collision.gameObject.SetActive(false);
        }
        //if (collision.CompareTag("HealingBullet"))
        //{
        //    Damage(-1);
        //    collision.gameObject.SetActive(false);
        //}
    }

    private void HandleBulletDamage(string tag)
    {
        switch (tag)
        {
            case "PistolPlayerBullet":
                Damage(PistolWeapon.Instance.Damage);
                break;
            case "ShotgunPlayerBullet":
                Damage(ShotgunWeapon.Instance.Damage);
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            Damage(_flamethrowerWeapon.Damage);
        }
    }

    public void Damage(int damage)
    {
        Health -= damage;
        DamageUI.Instance.ShowDamageOnEnemy(transform.position);
        if (Health <= 0)
        {
            Die();
        }
        if (gameObject.activeSelf)
        {
            _enemyUI.CallDamageFlash();
        }
    }

    protected void Die()
    {
        EnemySpawner.EnemyCount--;
        gameObject.SetActive(false);
    }
}
