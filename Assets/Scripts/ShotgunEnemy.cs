using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunEnemy : Enemy
{
    [SerializeField]
    Transform target;

    Vector2 _minHeight;
    Vector2 _maxHeight;

    Vector2 arrivalPoint;

    private bool movingToEnd = true;

    [SerializeField]
    float verticalMoveSpeed = 5f;

    public float spreadAngle;
    public int bulletsPerShot = 8;

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
        StartCoroutine(RepeatingShootAfterDelay());
    }
    private void Update()
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
        }
    }

    void Shoot()
    {
        foreach (var obj in gameObjectsPool.pool)
        {          
                if (!obj.activeSelf)
                {
                    obj.SetActive(true);
                    obj.transform.position = transform.position;
                    Quaternion spreadRotation = Quaternion.Euler(0f, 0f, Random.Range(-spreadAngle / 2, spreadAngle / 2));
                    obj.GetComponent<Bullet>().direction = (spreadRotation * (target.transform.position - obj.transform.position)).normalized;

                    Debug.Log("Shoot");
                    break;
                } 
                
        }
    }

    private void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    private void FlyToTheScreen()
    {
        float distance = Vector2.Distance(transform.position, arrivalPoint);

        if (distance > 0.01f)
            transform.position = Vector2.MoveTowards(transform.position, arrivalPoint, verticalMoveSpeed * Time.deltaTime);
        else 
            currentState = EnemyStates.Attacking;
    }
    IEnumerator RepeatingShootAfterDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / fireRate);
            for (int i = 0; i < bulletsPerShot; i++)
            {
                Shoot();
            }
        }
    }
}
