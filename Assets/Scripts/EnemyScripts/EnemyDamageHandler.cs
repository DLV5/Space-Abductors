using UnityEngine;

public class EnemyDamageHandler : MonoBehaviour, IDamageable
{
    protected EnemyUI _enemyUI;

    [SerializeField] protected int _maxHealth;
    protected int _health;
    public int Health
    {
        get => _health;
        set => _health = value;
    }

    private static FlamethrowerWeapon _flamethrowerWeapon;

    /// <summary>
    /// Variable for counting frames before taking damage for flamethrower
    /// </summary>
    private int _frameCounter = 0;

    /// <summary>
    /// Numbers of frames before taking fire damage
    /// </summary>
    private static int _framesToWait = 0;

    protected virtual void Awake()
    {
        _enemyUI = GetComponent<EnemyUI>();
        _flamethrowerWeapon = FindObjectOfType<FlamethrowerWeapon>(true);
    }

    private void OnEnable()
    {
        _health = _maxHealth;
        _framesToWait = 60 / FlamethrowerWeapon.Instance.DamageTicksPerSecond;
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
                Damage(ShotgunWeapon.ShotgunInstance.Damage);
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            ++_frameCounter;
            if (_frameCounter >= _framesToWait)
            {
                Damage(_flamethrowerWeapon.Damage);
                // Reset the frame counter
                _frameCounter = 0;
            }
        }
    }

    public void Damage(int damage)
    {
        Health -= damage;
        DamageUI.Instance.ShowDamageOnEnemy(transform.position, damage);
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
