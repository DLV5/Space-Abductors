using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
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

    public ObjectPool enemyPool;
    public ObjectPool bulletPool;

    void Awake()
    {
        enemyPool = new ObjectPool(enemyPrefabs[0].numberOfCopies, enemyPrefabs[0].objectToCopy);
        bulletPool = new ObjectPool(bulletPrefabs[0].numberOfCopies, bulletPrefabs[0].objectToCopy);
        for (int i = 1; i < enemyPrefabs.Count; i++)
        {
            enemyPool += new ObjectPool(enemyPrefabs[i].numberOfCopies, enemyPrefabs[i].objectToCopy);
        }
        for (int i = 1; i < enemyPrefabs.Count; i++)
        {
            bulletPool += new ObjectPool(bulletPrefabs[i].numberOfCopies, bulletPrefabs[i].objectToCopy);
        }
    }
}
