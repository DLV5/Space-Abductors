using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] protected float _flashTime = 0.25f;

    private SpriteRenderer _spriteRenderer;
    private Material _material;

    private void Awake()
    {
        Initialize();
    }

    public void CallDamageFlash()
    {
        StartCoroutine(StartDamageFlash());
    }

    protected virtual void Initialize()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _material = _spriteRenderer.material;
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
