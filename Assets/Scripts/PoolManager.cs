using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    private static bool hasInvoked = false;
    [Serializable]
    class PoolObject
    {
        public GameObject objectToCopy;
        public int numberOfCopies;

    }
    public static PoolManager Instance;
    PoolManager()
    {
        Instance = this;
    }
    [SerializeField]
    List<PoolObject> enemyPrefabs = new List<PoolObject>();

    [SerializeField]
    List<PoolObject> bulletPrefabs = new List<PoolObject>();

    public static ObjectPool enemyPool;
    public static ObjectPool bulletPool;

    void Awake()
    {
        if (hasInvoked) return;
        enemyPool = new ObjectPool(enemyPrefabs[0].numberOfCopies, enemyPrefabs[0].objectToCopy);
        bulletPool = new ObjectPool(bulletPrefabs[0].numberOfCopies, bulletPrefabs[0].objectToCopy);
        for (int i = 1; i < enemyPrefabs.Count; i++)
        {
            ObjectPool objects = new ObjectPool(enemyPrefabs[i].numberOfCopies, enemyPrefabs[i].objectToCopy);
            foreach (var item in objects.pool)
            {
                enemyPool.pool.Add(item);
            }
        }
        for (int i = 1; i < bulletPrefabs.Count; i++)
        {
            ObjectPool objects = new ObjectPool(bulletPrefabs[i].numberOfCopies, bulletPrefabs[i].objectToCopy);
            foreach (var item in objects.pool)
            {
                 bulletPool.pool.Add(item);
            }
        }
        hasInvoked = true;
    }
}
