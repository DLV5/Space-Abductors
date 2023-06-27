using System;
using System.Collections;
using UnityEngine;

public class EnemyAttacker : Attacker, IDamageable
{
    [TagSelector, SerializeField] protected string _bulletTagToShoot;
    protected EnemyBehavior currentState = EnemyBehavior.FlyingToTheScreen;
    protected static ObjectPool gameObjectsPool;

    protected GameObject _target;
    [SerializeField] protected int _health;
    public int Health 
    { 
        get => _health; 
        set => _health = value; 
    }
    public override float FireRate 
    { 
        get => _fireRate; 
        set => _fireRate = value; 
    }

    [SerializeField] protected float _flashTime = 0.25f;

    private SpriteRenderer _spriteRenderer;
    private Material _material;

    protected enum EnemyBehavior
    {
        FlyingToTheScreen = 0,
        Attacking = 1,
        Leaving = 2
    }

    protected virtual void Awake()
    {
        Initialize();
    }
    public void Damage(int damage)
    {
        Health -= damage;
        DamageUI.Instance.ShowDamageOnEnemy(transform.position);
        if (Health <= 0) 
        {
            EnemySpawner.EnemyCount--;
            gameObject.SetActive(false);
        }
        if(gameObject.activeSelf)
        {
            CallDamageFlash();
        }
    }

    public void CallDamageFlash()
    {
        StartCoroutine(StartDamageFlash());
    }
    protected IEnumerator StartDamageFlash()
    {
        if (_material == null || _spriteRenderer == null)
        { 
            Initialize();
        }
        float currentFlashAmount = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < _flashTime)
        {
            elapsedTime += Time.deltaTime;

            currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedTime / _flashTime));
            _material.SetFloat("_FlashAmount", currentFlashAmount);

            yield return null; 
        }
    }

    protected virtual void Initialize()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _material = _spriteRenderer.material;
        _material.SetFloat("_FlashAmount", 0);
    }

    protected override void Fire()
    {
        var obj = gameObjectsPool.GetPooledObjectByTag(_bulletTagToShoot);

        obj.transform.position = transform.position;
        obj.GetComponent<Bullet>().Direction = (_target.transform.position - obj.transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            //Damage(Weapon.Instance.Damage);
            collision.gameObject.SetActive(false);
        }
        if (collision.CompareTag("HealingBullet"))
        {
            Damage(-1);
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            Damage(1);
        }
    }
}
