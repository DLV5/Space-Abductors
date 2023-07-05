using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static ObjectsPool EnemyPool;
    public static ObjectsPool BulletPool;

    private static bool _hasInvoked = false;
    public static PoolManager Instance { get; private set; }
    [Serializable]
    private class PoolObject
    {
        [SerializeField]private GameObject _objectToCopy;
        public GameObject ObjectToCopy
        {
            get => _objectToCopy;
            set => _objectToCopy = value;
        }

        [SerializeField] private int _numberOfCopies;
        public int NumberOfCopies
        {
            get => _numberOfCopies;
            set => _numberOfCopies = value;
        }

    }
    [SerializeField] private List<PoolObject> _enemyPrefabs = new List<PoolObject>();
    [SerializeField] private List<PoolObject> _bulletPrefabs = new List<PoolObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        if (_hasInvoked) 
            return;
        EnemyPool = new ObjectsPool(_enemyPrefabs[0].NumberOfCopies, _enemyPrefabs[0].ObjectToCopy);
        BulletPool = new ObjectsPool(_bulletPrefabs[0].NumberOfCopies, _bulletPrefabs[0].ObjectToCopy);
        for (int i = 1; i < _enemyPrefabs.Count; i++)
        {
            var objects = new ObjectsPool(_enemyPrefabs[i].NumberOfCopies, _enemyPrefabs[i].ObjectToCopy);
            foreach (var item in objects.Pool)
            {
                EnemyPool.Pool.Add(item);
            }
        }
        for (int i = 1; i < _bulletPrefabs.Count; i++)
        {
            var objects = new ObjectsPool(_bulletPrefabs[i].NumberOfCopies, _bulletPrefabs[i].ObjectToCopy);
            foreach (var item in objects.Pool)
            {
                 BulletPool.Pool.Add(item);
            }
        }
        _hasInvoked = true;
    }
}
