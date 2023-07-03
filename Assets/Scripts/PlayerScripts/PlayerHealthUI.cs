using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerDamageHandler))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerHealthUI : MonoBehaviour
{
    private TextMeshProUGUI _hpText;

    private PlayerHealth _playerHealth;
    private PlayerDamageHandler _playerDamageHandler;

    private const float _flickerDuration = 0.1f;
    private float _flickerTimer = 0f;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        _playerDamageHandler = GetComponent<PlayerDamageHandler>();
        _hpText = GameObject.Find("HpText").GetComponent<TextMeshProUGUI>();
        _renderer = GetComponent<SpriteRenderer>();
        if (_hpText == null)
        {
            _hpText = GameObject.Find("HpText").GetComponent<TextMeshProUGUI>();
        }
        _hpText.text = "HP: " + _playerHealth.Health;
        _playerDamageHandler.Damaged += UpdateText;
        _playerDamageHandler.InvinvibilityDisabled += EnableRenderer;
    }

    private void Update()
    {
        if (_playerDamageHandler.IsInvincible)
        {
            Flicker();
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

    private void UpdateText()
    {
        _hpText.text = "HP: " + _playerHealth.Health;
    }

    private void EnableRenderer()
    {
        _renderer.enabled = true;
    }
}
