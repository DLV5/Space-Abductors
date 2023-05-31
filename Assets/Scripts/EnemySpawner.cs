using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    List<GameObject> enemyPrefabs = new List<GameObject>();
    List<GameObjectsPool>enemyObjectsPools = new List<GameObjectsPool>();

    [SerializeField]
    Collider2D spawnZone;

    [SerializeField]
    float SpawnDelay;
    
    [SerializeField]
    int SpawnCount;
    void Awake()
    {
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            GameObjectsPool gameObjectsPool = new GameObjectsPool(5, enemyPrefabs[i]);
            enemyObjectsPools.Add(gameObjectsPool);
        }
        StartCoroutine(SpawnInsideZone());
    }

    IEnumerator SpawnInsideZone()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnDelay);
            for (int i = 0; i < SpawnCount; i++)
            {
                Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], GetRandomPointInsideTheArea(spawnZone), Quaternion.identity);
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
}
