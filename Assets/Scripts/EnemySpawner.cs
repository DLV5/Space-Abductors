using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private static GameObjectsPool EnemyObjectPool;

    [SerializeField]
    Collider2D spawnZone;

    [SerializeField]
    float SpawnDelay;
    
    [SerializeField]
    int SpawnCount;
    void Awake()
    {
        StartCoroutine(SpawnInsideZone());
    }

    IEnumerator SpawnInsideZone()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnDelay);
            for (int i = 0; i < SpawnCount; i++)
            {
                SpawnEnemy();
            }
        }

    }

    Vector2 GetRandomPointInsideTheArea(Collider2D collider)
    {
        float randomX = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
        float randomY = Random.Range(collider.bounds.min.y, collider.bounds.max.y);
        Vector2 point = new Vector2(randomX, randomY);
        return point;
    }
    void SpawnEnemy()
    {
        foreach (var obj in EnemyObjectPool.pool)
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                obj.transform.position = GetRandomPointInsideTheArea(spawnZone);

                break;
            }
        }
    }
}
