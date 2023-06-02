using System.Collections;
using UnityEngine;

public class MovingEnemy : Enemy
{
    protected Vector2 _minHeight;
    protected Vector2 _maxHeight;

    protected Vector2 arrivalPoint;

    [Header("Movement")]

    [SerializeField]
    float verticalMoveSpeed = 5f;

    private bool movingToEnd = true;

    [Header("Escape settings")]

    [SerializeField]
    float timeBerofeEscape = 5f;

    [SerializeField]
    float escapeHorizontalSpeed = 0f;

    private void Awake()
    {
        _minHeight = Camera.main.ScreenToWorldPoint(Vector2.zero);
        _maxHeight = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
    }

    protected virtual void OnEnable()
    {
        currentState = EnemyStates.FlyingToTheScreen;
        arrivalPoint = GeneratePointToFly();
        StartCoroutine(WaitUntilEscape());
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyStates.FlyingToTheScreen:
                FlyToTheScreen();
                break;
            case EnemyStates.Attacking:
                FlyWhenAttacking();
                break;
            case EnemyStates.Leaving:
                FlyOutOfTheScreen();
                break;
        }
    }
    protected virtual void FlyToTheScreen()
    {
        float distance = Vector2.Distance(transform.position, arrivalPoint);

        if (distance > 0.01f)
            transform.position = Vector2.MoveTowards(transform.position, arrivalPoint, verticalMoveSpeed * Time.deltaTime);
        else currentState = EnemyStates.Attacking;
    }

    protected virtual void FlyWhenAttacking()
    {
        Vector2 targetPosition = movingToEnd ? _maxHeight : _minHeight;
        float distance = Vector2.Distance(transform.position, new Vector2(transform.position.x, targetPosition.y));

        if (distance > 0.01f)
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, targetPosition.y), verticalMoveSpeed * Time.deltaTime);
        else
            movingToEnd = !movingToEnd; // Reverse the movement direction
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
        transform.position += Time.deltaTime * Vector3.left * escapeHorizontalSpeed;
        StartCoroutine(CheckIsInTheBoundOfTheScreen());
    }

    IEnumerator WaitUntilEscape()
    {
        yield return new WaitForSeconds(timeBerofeEscape);
        currentState = EnemyStates.Leaving;

    }

    IEnumerator CheckIsInTheBoundOfTheScreen()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

            if (!Screen.safeArea.Contains(pos)) gameObject.SetActive(false);
        }
    }
}
