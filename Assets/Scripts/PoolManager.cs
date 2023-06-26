using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static bool _hasInvoked = false;
    [Serializable]
    private class PoolObject
    {
        public GameObject ObjectToCopy;
        public int NumberOfCopies;

    }
    public static PoolManager Instance;
    PoolManager()
    {
        Instance = this;
    }
    [SerializeField] private List<PoolObject> _enemyPrefabs = new List<PoolObject>();

    [SerializeField] private List<PoolObject> _bulletPrefabs = new List<PoolObject>();

    public static ObjectPool EnemyPool;
    public static ObjectPool BulletPool;

    void Awake()
    {
        if (_hasInvoked) return;
        EnemyPool = new ObjectPool(_enemyPrefabs[0].NumberOfCopies, _enemyPrefabs[0].ObjectToCopy);
        BulletPool = new ObjectPool(_bulletPrefabs[0].NumberOfCopies, _bulletPrefabs[0].ObjectToCopy);
        for (int i = 1; i < _enemyPrefabs.Count; i++)
        {
            var objects = new ObjectPool(_enemyPrefabs[i].NumberOfCopies, _enemyPrefabs[i].ObjectToCopy);
            foreach (var item in objects.Pool)
            {
                EnemyPool.Pool.Add(item);
            }
        }
        for (int i = 1; i < _bulletPrefabs.Count; i++)
        {
            var objects = new ObjectPool(_bulletPrefabs[i].NumberOfCopies, _bulletPrefabs[i].ObjectToCopy);
            foreach (var item in objects.Pool)
            {
                 BulletPool.Pool.Add(item);
            }
        }
        _hasInvoked = true;
    }
}
