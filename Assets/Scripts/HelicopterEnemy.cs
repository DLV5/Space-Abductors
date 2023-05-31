using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class HelicopterEnemy : Enemy
{
    Transform target;

    Vector2 _minHeight;
    Vector2 _maxHeight;

    Vector2 arrivalPoint;

    [Header("Movement")]

    [SerializeField]
    float verticalMoveSpeed = 5f;

    private bool movingToEnd = true;

    [Header("Escape settings")]

    [SerializeField]
    float timeBerofeEscape = 5f;

    [SerializeField]
    float escapeHorizontalSpeed = 0f;

    private static GameObjectsPool gameObjectsPool;

    EnemyStates currentState = EnemyStates.FlyingToTheScreen;

    enum EnemyStates
    {
        FlyingToTheScreen,
        Attacking,
        Leaving
    }
    private void Awake()
    {
        _minHeight = Camera.main.ScreenToWorldPoint(Vector2.zero);
        _maxHeight = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
    }

    private void Start()
    {
        if (gameObjectsPool == null)
            gameObjectsPool = GameManager.Instance.GetGameObjectsPool("BaseBullet");
    }

    private void OnEnable()
    {
        currentState = EnemyStates.FlyingToTheScreen;
        arrivalPoint = GeneratePointToFly();
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        StartCoroutine(RepeatingShootAfrterDelay());
        StartCoroutine(WaitUntilEscape());
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyStates.FlyingToTheScreen:
                FlyToTheScreen();
                break;
            case EnemyStates.Attacking:
                Vector2 targetPosition = movingToEnd ? _maxHeight : _minHeight;
                float distance = Vector2.Distance(transform.position, new Vector2(transform.position.x, targetPosition.y));

                if (distance > 0.01f)                
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, targetPosition.y), verticalMoveSpeed * Time.deltaTime);                
                else
                    movingToEnd = !movingToEnd; // Reverse the movement direction
                break;
            case EnemyStates.Leaving:
                FlyOutOfTheScreen();
                break;
            }
    }

    private void FlyToTheScreen()
    {
        float distance = Vector2.Distance(transform.position, arrivalPoint);

        if (distance > 0.01f)
            transform.position = Vector2.MoveTowards(transform.position, arrivalPoint, verticalMoveSpeed * Time.deltaTime);
        else currentState = EnemyStates.Attacking;
    }

    private Vector2 GeneratePointToFly()
    {
        float x = UnityEngine.Random.Range(Screen.width, Screen.width / 2);
        float y = UnityEngine.Random.Range(_minHeight.y, _maxHeight.y);
        Vector2 arrivalPoint = Camera.main.ScreenToWorldPoint(new Vector2(x, y));
        return arrivalPoint;
    }


    private void FlyOutOfTheScreen()
    {
        transform.position += Time.deltaTime * Vector3.left * escapeHorizontalSpeed;
        StartCoroutine(CheckIsInTheBoundOfTheScreen());
    }

    void Shoot()
    {       
        foreach(var obj in gameObjectsPool.pool) {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                obj.transform.position = transform.position;
                obj.GetComponent<Bullet>().direction = (target.transform.position - obj.transform.position).normalized;

                break;
            }
        }
    }
    IEnumerator WaitUntilEscape()
    {
        yield return new WaitForSeconds(timeBerofeEscape);
        currentState = EnemyStates.Leaving;

    }
    IEnumerator RepeatingShootAfrterDelay()
    {
        while(true)
        {
            yield return new WaitForSeconds(1 / fireRate);
            Shoot();
        }
    }

    IEnumerator CheckIsInTheBoundOfTheScreen()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            Debug.Log("Co");
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

            if (!Screen.safeArea.Contains(pos)) gameObject.SetActive(false);
        }
    }
}
