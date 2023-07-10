using System;
using UnityEngine;

public class CowStealing : MonoBehaviour
{
    static public event Action StartedCowCapture;
    static public event Action FinishedCowCapture;

    private PlayerHealth _player; // For healing
    private Cow _currentCow;
    private bool _capturing = false;

    private void Awake()
    {
        StartedCowCapture += ToggleCapturing;
        FinishedCowCapture += ToggleCapturing;
    }

    private void OnDisable()
    {
        StartedCowCapture -= ToggleCapturing;
        FinishedCowCapture -= ToggleCapturing;
    }

    private void Start()
    {
        _player = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StealCow();
        }
        if (!_capturing)
        {
            if (!_currentCow.IsMoving)
            {
                Destroy(_currentCow.gameObject);
                Skills.Instance.AddSkillpoints(1);
                _player.Health += 1;
                FinishedCowCapture?.Invoke();
                --EnemySpawner.EnemyCount;
            }
        }
    }

    private void StealCow()
    {
        if (_capturing) 
            return;
        var hit = Physics2D.Raycast(transform.position, Vector2.down);
        var cow = hit.collider;
        if (cow == null) 
            return;
        if (cow.CompareTag("Cow"))
        {
            _currentCow = cow.gameObject.GetComponent<Cow>();
            StartedCowCapture?.Invoke();
            _currentCow.IsMoving = true;
        }
    }

    private void ToggleCapturing()
    {
        _capturing = !_capturing;
    }

}
