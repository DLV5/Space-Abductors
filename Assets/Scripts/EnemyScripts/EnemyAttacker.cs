using System;
using System.Collections;
using UnityEngine;

public class EnemyAttacker : Attacker, IDamageable
{
    [SerializeField] protected int _health;
    public int Health { get => _health; set => _health = value; }

    [SerializeField] protected float _flashTime = 0.25f;

    private SpriteRenderer _spriteRenderer;
    private Material _material;

    protected EnemyBehavior currentState = EnemyBehavior.FlyingToTheScreen;
    [Flags]
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

    protected void Initialize()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _material = _spriteRenderer.material;
        _material.SetFloat("_FlashAmount", 0);
    }
    public void Damage(int damage)
    {
        Health -= damage;
        DamageUI.Instance.ShowDamageOnEnemy(transform.position);
        if (Health <= 0) 
        {
            //StopCoroutine(DamageFlasher());
            EnemySpawner.EnemyCount--;
            gameObject.SetActive(false);
        }
        if(gameObject.activeSelf)
                CallDamageFlash();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Damage(Weapon.Instance.Damage);
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

    public void CallDamageFlash()
    {
        StartCoroutine(StartDamageFlash());
    }
    protected IEnumerator StartDamageFlash()
    {
        if (_material == null || _spriteRenderer == null) Initialize();
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
    
}
