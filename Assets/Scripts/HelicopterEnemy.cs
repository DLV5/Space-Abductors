using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterEnemy : Enemy
{
    [SerializeField]
    Transform target;

    [Header("Movement")]

    [SerializeField]
    float frequency = 5f;

    [SerializeField]
    float magnitude = 5f;

    [SerializeField]
    float offset = 0f;

    [Header("Escape settings")]

    [SerializeField]
    float timeBerofeEscape = 5f;

    [SerializeField]
    float escapeSpeed = 0f;


    private Vector3 startPosition;

    private static GameObjectsPool gameObjectsPool;

    EnemyStates currentState = EnemyStates.Attacking;

    enum EnemyStates
    {
        Attacking,
        Leaving
    }
    void Start()
    {
        if(gameObjectsPool == null)
            gameObjectsPool = GameManager.Instance.GetGameObjectsPool(bulletPrefab);

        startPosition = transform.position;
        StartCoroutine(RepeatingShootAfrterDelay());
        StartCoroutine(WaitUntilEscape());
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyStates.Attacking:
                transform.position = startPosition + transform.up * Mathf.Sin(Time.time * frequency + offset) * magnitude;
                break;
            case EnemyStates.Leaving:
                transform.position += Time.deltaTime * Vector3.left * escapeSpeed;
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
