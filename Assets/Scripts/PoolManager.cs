using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [Serializable]
    class PoolObject
    {
        public GameObject objectToCope;
        public int numberOfCopies;
    }
    public static PoolManager Instance;
    PoolManager()
    {
        Instance = this;
    }
    [SerializeField]
    List<PoolObject> objectsPrefabs = new List<PoolObject>();

    List<GameObjectsPool> gameObjectsPools = new List<GameObjectsPool>();
    void Awake()
    {
        for (int i = 0; i < objectsPrefabs.Count; i++)
        {
            GameObjectsPool gameObjectsPool = new GameObjectsPool(objectsPrefabs[i].numberOfCopies, objectsPrefabs[i].objectToCope);
            gameObjectsPools.Add(gameObjectsPool);
        }
    }

    public GameObjectsPool GetGameObjectsPool(string objectTag)
    {
        for (int i = 0; i < gameObjectsPools.Count; i++)
        {
            if (objectTag == gameObjectsPools[i].pool[0].tag) return gameObjectsPools[i];
        }

        throw new System.IndexOutOfRangeException();
    }
}
