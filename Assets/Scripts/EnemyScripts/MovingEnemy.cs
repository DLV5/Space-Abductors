using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyMovingBehavior
{
    CircleMove,
    ReverseCircleMove,
    CurveMove,
    ReverseCurveMove,
    StandAtTopLeft,
    StandAtTopMiddle,
    StandAtTopRight,
    StandAtMiddleLeft,
    StandAtCenter,
    StandAtMiddleRight,
    StandAtButtomLeft,
    StandAtButtomMiddle,
    StandAtButtomRight
}
public class MovingEnemy : EnemyAttacker
{
    public static EnemyMovingBehavior Behavior { get; set; } = EnemyMovingBehavior.StandAtCenter;

    public static EnemyPathData EnemyPathData { get; set; }

    protected GameObject _movingPath;
    private Vector3 _nextWayPointPos;
    private int _wayPointCounter;
    private int _lastWayPoint;

    protected Vector2 _minHeight;
    protected Vector2 _maxHeight;

    protected Vector2 _arrivalPoint;

    [Header("Movement")]

    [SerializeField] private float _verticalMoveSpeed = 5f;

    [Header("Escape settings")]

    [SerializeField] private float _timeBerofeEscape = 5f;

    [SerializeField] private float _escapeHorizontalSpeed = 5f;

    private bool _isMovingToEnd = true;

    protected override void Awake()
    {
        //_minHeight = Camera.main.ScreenToWorldPoint(Vector2.zero);
        //_maxHeight = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
    }

    protected virtual void OnEnable()
    {
        _movingPath = EnemyPathData.GetPath(Behavior);
        _nextWayPointPos = GetPathPoint(_wayPointCounter);
        _lastWayPoint = _movingPath.transform.childCount;
        currentState = EnemyBehavior.FlyingToTheScreen;
        _arrivalPoint = GeneratePointToFly();
        StartCoroutine(WaitUntilEscape());
    }

    private void OnDisable()
    {
        _lastWayPoint = 0;
        _wayPointCounter = 0;
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
        if(_lastWayPoint == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                _nextWayPointPos, _verticalMoveSpeed * Time.deltaTime);
            return;
        }
        if (_wayPointCounter < _lastWayPoint- 1)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                _nextWayPointPos, _verticalMoveSpeed * Time.deltaTime);
            if ((transform.position - _nextWayPointPos).magnitude < 0.1f)
            {
                ++_wayPointCounter;
                _nextWayPointPos = GetPathPoint(_wayPointCounter);
            }
        } else
        {
            _wayPointCounter = 0;
            _nextWayPointPos = GetPathPoint(_wayPointCounter);
        }
    }

    private Vector3 GetPathPoint(int numberOfPoint)
    {
        return _movingPath.transform.GetChild(numberOfPoint).position;
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

            if (!Screen.safeArea.Contains(pos))
            {
                EnemySpawner.EnemyCount--;
                gameObject.SetActive(false);
            }
        }
    }
}
