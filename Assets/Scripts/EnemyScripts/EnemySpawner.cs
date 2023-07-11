using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySpawnerUI))]
public class EnemySpawner : MonoBehaviour
{
    [Serializable]
    private class EnemySettings
    {
        [Tooltip("Choose enemy tag you wanna spawn")]
        [TagSelector] public string Tag;

        [Header("Chance to spawn between 0 and 1")]
        public float ChanceToSpawn;
    }
    [HideInInspector] public bool IsSpawning = true;
    [HideInInspector] public bool HasCowSpawned = false;
    public GameObject CowPrefab;

    [SerializeField] protected EnemyPathData _pathData;

    private static ObjectsPool _enemyObjectPool;
    [SerializeField] private List<EnemySettings> _enemySettings;
    [SerializeField] private Collider2D _spawnZone;
    [SerializeField] private float _spawnDelay;  
    [SerializeField] private int _spawnCount;
    [SerializeField] private float _cowSpawnDelay;

    [SerializeField] private EnemyWave[] _waves = new EnemyWave[] {};
    private Gamemode _spawnMode = Gamemode.WaveSpawn;
    public event Action<int> WaveSpawned;

    private static int _enemyCount = 0;
    private int _waveCount = 0;

    public static int EnemyCount 
    { 
        get => _enemyCount; 
        set => _enemyCount = value;
    }

    public int NumberOfWaves
    {
        get => _waves.Length;
    }

    public enum Gamemode
    {
        EndlessSpawn,
        WaveSpawn
    }

    private void OnEnable()
    {
        MovingEnemy.EnemyPathData = _pathData;
    }

    void Start()
    {
        switch(PlayerPrefs.GetString("Mode", "Story"))
        {
            case "Story":
                _spawnMode = Gamemode.WaveSpawn;
                break;
            case "EndlessEasy":
                _spawnMode = Gamemode.EndlessSpawn;
                break;
            case "EndlessNormal":
                _spawnMode = Gamemode.EndlessSpawn;
                break;
            case "EndlessHard":
                _spawnMode = Gamemode.EndlessSpawn;
                break;
        }

        DeactivateAllEnemies();
        _enemyObjectPool = PoolManager.EnemyPool;
        switch (_spawnMode)
        {
            case Gamemode.WaveSpawn:
                StartCoroutine(SpawnWaves());
                break;
            case Gamemode.EndlessSpawn:
                StartCoroutine(SpawnInsideZone());
                StartCoroutine(WaitAndSpawnCow());
                break;
            default:
                Debug.Log("Default invoke enemySpawner");
                break;
        }
    }

    private void DeactivateAllEnemies()
    {
        if (_enemyObjectPool == null) 
            return;
        
        foreach(GameObject enemy in _enemyObjectPool.Pool)
        {
            enemy.SetActive(false);
        }

        _enemyCount = 0;
    }

    private IEnumerator SpawnWaves()
    {
        foreach(var wave in _waves)
        {
            WaveSpawned?.Invoke(++_waveCount);
            foreach (var wavePart in wave.WaveParts)
            {
                for (int i = 0; i < wavePart.EnemyCount; i++)
                {
                    if(wavePart.EnemyTag == "Cow")
                    {
                        StartCoroutine(WaitAndSpawnCow());
                    } else
                    {
                        MovingEnemy.Behavior = wavePart.MoveBehavior;
                        SpawnEnemy(wavePart.EnemyTag);
                    }
                    ++_enemyCount;
                    Debug.Log(_enemyCount);
                    yield return new WaitForSeconds(wavePart.DelayBetweenSpawn);
                }
                yield return new WaitUntil(() => _enemyCount == 0);
            }
        }
    }

    private void SpawnEnemy()
    {
        var obj = ChooseObject(_enemySettings);
        obj.transform.position = GetRandomPointInsideTheArea(_spawnZone);
    }

    private void SpawnEnemy(string tag)
    {
        var obj = _enemyObjectPool.GetPooledObjectByTag(tag);

        obj.transform.position = GetRandomPointInsideTheArea(_spawnZone);
        
    }

    private static GameObject ChooseObject(List<EnemySettings> enemies)
    {
        float totalProbability = 0;
        foreach (var probability in enemies)
        {
            totalProbability += probability.ChanceToSpawn;
        }

        float cumulativeProbability = 0;
        float randomNum = UnityEngine.Random.Range(0f, 1f);

        foreach (var obj in enemies)
        {
            cumulativeProbability += obj.ChanceToSpawn / totalProbability;
            if (randomNum < cumulativeProbability)
            {
                GameObject rez = _enemyObjectPool.GetPooledObjectByTag(obj.Tag);
                return rez;
            }
        }

        return null; // In case of error or no object selected
    }

    //private bool IsAllEnemiesDead()
    //{
    //    foreach (var pool in enemyObjectPool.pool)
    //    {
    //        if(pool.activeSelf) return false;
    //    }
    //    return true;
    //}

    private IEnumerator SpawnInsideZone()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnDelay);
            if (IsSpawning)
            {
                for (int i = 0; i < _spawnCount; i++)
                {
                    SpawnEnemy();
                }
            }
        }

    }

    private Vector2 GetRandomPointInsideTheArea(Collider2D collider)
    {
        float randomX = UnityEngine.Random.Range(collider.bounds.min.x, collider.bounds.max.x);
        float randomY = UnityEngine.Random.Range(collider.bounds.min.y, collider.bounds.max.y);
        var point = new Vector2(randomX, randomY);
        return point;
    }

    private IEnumerator WaitAndSpawnCow()
    {
        yield return new WaitForSeconds(_cowSpawnDelay);
        if (!HasCowSpawned)
        {
            IsSpawning = false;
            SpawnCow();
            HasCowSpawned = true;
        }
    }

    private void SpawnCow()
    {
        Vector2 cowSpawnPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, 100));
        GameObject.Instantiate(CowPrefab, cowSpawnPosition, Quaternion.identity);
    }
}
