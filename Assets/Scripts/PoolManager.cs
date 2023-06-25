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
        public GameObject ObjectToCopy;
        public int NumberOfCopies;

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
        enemyPool = new ObjectPool(enemyPrefabs[0].NumberOfCopies, enemyPrefabs[0].ObjectToCopy);
        bulletPool = new ObjectPool(bulletPrefabs[0].NumberOfCopies, bulletPrefabs[0].ObjectToCopy);
        for (int i = 1; i < enemyPrefabs.Count; i++)
        {
            ObjectPool objects = new ObjectPool(enemyPrefabs[i].NumberOfCopies, enemyPrefabs[i].ObjectToCopy);
            foreach (var item in objects.Pool)
            {
                enemyPool.Pool.Add(item);
            }
        }
        for (int i = 1; i < bulletPrefabs.Count; i++)
        {
            ObjectPool objects = new ObjectPool(bulletPrefabs[i].NumberOfCopies, bulletPrefabs[i].ObjectToCopy);
            foreach (var item in objects.Pool)
            {
                 bulletPool.Pool.Add(item);
            }
        }
        hasInvoked = true;
    }
}
