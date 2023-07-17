using System.Collections;
using UnityEngine;
using UnityEngine.UI;
 

public class EnemyUI : MonoBehaviour
{
    [SerializeField] protected float _flashTime = 0.25f;

    [SerializeField] protected bool _shouldShowHealthBar = false;
    public bool ShouldShowHealthBar
    {
        get => _shouldShowHealthBar;
        private set => _shouldShowHealthBar = value;
    }

    private Slider _healthBarSlider;

    private SpriteRenderer _spriteRenderer;
    private Material _material;

    private void OnEnable()
    {
        Initialize();
    }

    private void OnDisable()
    {
        Unitialize();
    }

    public void CallDamageFlash()
    {
        StartCoroutine(StartDamageFlash());
    }

    public void SetSliderHealth(int value)
    {
        _healthBarSlider.value = value;
    }

    protected virtual void Initialize()
    {
        if (_shouldShowHealthBar)
        {
            _healthBarSlider = FindAnyObjectByType<Slider>(FindObjectsInactive.Include) ;
            _healthBarSlider.gameObject.SetActive(true);
        }

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _material = _spriteRenderer.material;
        _material.SetFloat("_FlashAmount", 0);
    }
    
    protected virtual void Unitialize()
    {
        if (_shouldShowHealthBar)
        {
            _healthBarSlider.gameObject.SetActive(false);
        }

        _material.SetFloat("_FlashAmount", 0);
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


}
