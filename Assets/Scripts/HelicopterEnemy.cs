using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterEnemy : Enemy
{
    [SerializeField]
    Transform target;

    Vector3 _minHeight;
    Vector3 _maxHeight;

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

    EnemyStates currentState = EnemyStates.Attacking;

    enum EnemyStates
    {
        Attacking,
        Leaving
    }
    private void Awake()
    {
        _minHeight = Camera.main.ScreenToWorldPoint(Vector2.zero);
        _maxHeight = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
    }        
    void Start()
    {
        if(gameObjectsPool == null)
            gameObjectsPool = GameManager.Instance.GetGameObjectsPool(bulletPrefab);

        StartCoroutine(RepeatingShootAfrterDelay());
        StartCoroutine(WaitUntilEscape());
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyStates.Attacking:
                Vector3 targetPosition = movingToEnd ? _maxHeight : _minHeight;
                float distance = Vector3.Distance(transform.position, new Vector3(transform.position.x, targetPosition.y));

                if (distance > 0.01f)                
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetPosition.y), verticalMoveSpeed * Time.deltaTime);                
                else
                    movingToEnd = !movingToEnd; // Reverse the movement direction
                break;
            case EnemyStates.Leaving:
                transform.position += Time.deltaTime * Vector3.left * escapeHorizontalSpeed;
                break;
            }
    }



    void Shoot()
    {       
        foreach(var gameObject in gameObjectsPool.pool) {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                gameObject.transform.position = transform.position;
                gameObject.GetComponent<Bullet>().direction = (target.transform.position - gameObject.transform.position).normalized;

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
}
