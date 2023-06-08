using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Serializable]
    class EnemySettings
    {
        public string enemyName;

        [Header("Chance to spawn between 0 and 1")]
        public float chanceToSpawn;
    }
    private static ObjectPool enemyObjectPool;
    [SerializeField]
    private List<EnemySettings> enemySettings;

    [SerializeField]
    Collider2D spawnZone;

    [SerializeField]
    float SpawnDelay;
    
    [SerializeField]
    int SpawnCount;

    public GameObject cowPrefab;
    [SerializeField]
    private float cowSpawnDelay;

    public bool spawning = true;
    [HideInInspector]
    public bool cowSpawned = false;

    public enum EnemyTypes
    {
        HelicopterEnemy,
        ShootGunEnemy,
        NumberOfEnemyTypes
    }
    void Start()
    {
        enemyObjectPool = PoolManager.enemyPool;
        StartCoroutine(SpawnInsideZone());
        StartCoroutine(WaitAndSpawnCow());
    }

    IEnumerator SpawnInsideZone()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnDelay);
            if (spawning)
            {
                for (int i = 0; i < SpawnCount; i++)
                {
                    SpawnEnemy();
                }
            }
        }

    }

    IEnumerator WaitAndSpawnCow()
    {
        while (true)
        {
            yield return new WaitForSeconds(cowSpawnDelay);
            if (!cowSpawned)
            {
                spawning = false;
                SpawnCow();
                cowSpawned = true;
            }
        }
    }

    Vector2 GetRandomPointInsideTheArea(Collider2D collider)
    {
        float randomX = UnityEngine.Random.Range(collider.bounds.min.x, collider.bounds.max.x);
        float randomY = UnityEngine.Random.Range(collider.bounds.min.y, collider.bounds.max.y);
        Vector2 point = new Vector2(randomX, randomY);
        return point;
    }
    void SpawnEnemy()
    {
        GameObject obj = ChooseObject(enemySettings); 
        obj.transform.position = GetRandomPointInsideTheArea(spawnZone);
    }

    private void SpawnCow()
    {
        Vector2 cowSpawnPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, 100));
        GameObject.Instantiate(cowPrefab, cowSpawnPosition, Quaternion.identity);
    }

    static GameObject ChooseObject(List<EnemySettings> enemies)
    {
        float totalProbability = 0;
        foreach (var probability in enemies)
        {
            totalProbability += probability.chanceToSpawn;
        }

        float cumulativeProbability = 0;
        float randomNum = UnityEngine.Random.Range(0f, 1f);

        foreach (var obj in enemies)
        {
            cumulativeProbability += obj.chanceToSpawn / totalProbability;
            if (randomNum < cumulativeProbability)
            {
                GameObject rez = enemyObjectPool.GetPooledObjectByTag(obj.enemyName);
                return rez;
            }
        }

        return null; // In case of error or no object selected
    }
}
