using System.Collections;
using UnityEngine;
using TMPro;

public class Ship : MonoBehaviour, IDamageable
{
    [SerializeField] private TextMeshProUGUI _hpText;

    private const float _flickerDuration = 0.1f;
    private float _flickerTimer = 0f;
    private SpriteRenderer _renderer;

    [SerializeField] private int _health = 3;
    public int Health 
    {
        get => _health; 
        set => _health = value;
    }
    public bool IsInvincible { get; set; } = false;
    //public int Damage; where this used?


    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        if (_hpText == null)
        {
            _hpText = GameObject.Find("HpText").GetComponent<TextMeshProUGUI>();
        }
        _hpText.text = "HP: " + Health;
    }

    private void Update()
    {
        if (IsInvincible)
        {
            Flicker();
        }
    }

    public void Damage(int damage)
    {
        if (IsInvincible) 
            return;
        Health -= damage;
        IsInvincible = true;
        StartCoroutine(DisableInvincibilityAfterTime(2f));
        _hpText.text = "HP: " + Health;
        if (Health <= 0)
        {
            UIManager.Instance.DeathScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void Flicker()
    {
        if (_flickerTimer < _flickerDuration)
        {
            _flickerTimer += Time.deltaTime;
        }
        else
        {
            _renderer.enabled = !_renderer.enabled;
            _flickerTimer = 0f;
        }
    }

    private IEnumerator DisableInvincibilityAfterTime(float invincibilityDuration)
    {
        yield return new WaitForSeconds(invincibilityDuration);
        IsInvincible = false;
        _renderer.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BaseBullet") || collision.CompareTag("ShotGunBullet") || collision.CompareTag("RocketMissle"))
        {
            Damage(1);
            collision.gameObject.SetActive(false);
        }
        if (collision.CompareTag("HealingBullet"))
        {
            Damage(-1);
            collision.gameObject.SetActive(false);
        }
    }
}
