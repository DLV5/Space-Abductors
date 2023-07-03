using System.Collections;
using UnityEngine;

public class MovingEnemy : EnemyAttacker
{
    protected Vector2 _minHeight;
    protected Vector2 _maxHeight;

    protected Vector2 _arrivalPoint;

    [Header("Movement")]

    [SerializeField] private float _verticalMoveSpeed = 5f;

    [Header("Escape settings")]

    [SerializeField] private float _timeBerofeEscape = 5f;

    [SerializeField] private float _escapeHorizontalSpeed = 5f;

    private bool _isMovingToEnd = true;

    protected virtual void Awake()
    {
        _minHeight = Camera.main.ScreenToWorldPoint(Vector2.zero);
        _maxHeight = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
    }

    protected virtual void OnEnable()
    {
        currentState = EnemyBehavior.FlyingToTheScreen;
        _arrivalPoint = GeneratePointToFly();
        StartCoroutine(WaitUntilEscape());
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyBehavior.FlyingToTheScreen:
                FlyToTheScreen();
                break;
            case EnemyBehavior.Attacking:
                FlyWhenAttacking();
                break;
            case EnemyBehavior.Leaving:
                FlyOutOfTheScreen();
                break;
        }
    }
    protected virtual void FlyToTheScreen()
    {
        float distance = Vector2.Distance(transform.position, _arrivalPoint);

        if (distance > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, _arrivalPoint, _verticalMoveSpeed * Time.deltaTime);
        }
        else
        {
            currentState = EnemyBehavior.Attacking;
        }
    }

    protected virtual void FlyWhenAttacking()
    {
        Vector2 targetPosition = _isMovingToEnd ? _maxHeight : _minHeight;
        float distance = Vector2.Distance(transform.position, new Vector2(transform.position.x, targetPosition.y));

        if (distance > 0.01f)
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, targetPosition.y), _verticalMoveSpeed * Time.deltaTime);
        else
            _isMovingToEnd = !_isMovingToEnd; // Reverse the movement direction
    }
    protected virtual Vector2 GeneratePointToFly()
    {
        float x = Random.Range(Screen.width, Screen.width / 2);
        float y = Random.Range(_minHeight.y, _maxHeight.y);
        Vector2 arrivalPoint = Camera.main.ScreenToWorldPoint(new Vector2(x, y));
        return arrivalPoint;
    }


    protected virtual void FlyOutOfTheScreen()
    {
        transform.position += Time.deltaTime * Vector3.left * _escapeHorizontalSpeed;
        StartCoroutine(CheckIsInTheBoundOfTheScreen());
    }

    private IEnumerator WaitUntilEscape()
    {
        yield return new WaitForSeconds(_timeBerofeEscape);
        currentState = EnemyBehavior.Leaving;

    }

    private IEnumerator CheckIsInTheBoundOfTheScreen()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

            if (!Screen.safeArea.Contains(pos)) gameObject.SetActive(false);
        }
    }
}
