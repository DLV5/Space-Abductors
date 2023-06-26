using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public List<GameObject> Pool { get; private set; }
    private GameObject _prefab;
    public ObjectPool(int initialSize, GameObject prefab)
    {
        _prefab = prefab;
        Pool = new List<GameObject>();

        for (int i = 0; i < initialSize; i++)
        {
            CreatePooledObject();
        }
    }

    private GameObject CreatePooledObject()
    {
        var obj = UnityEngine.Object.Instantiate(_prefab);
        GameObject.DontDestroyOnLoad(obj);
        obj.SetActive(false);
        Pool.Add(obj);
        return obj;
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in Pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        return CreatePooledObject();
    }

    public GameObject GetPooledObjectByTag(string tag)
    {
        foreach (GameObject obj in Pool)
        {
            if (!obj.activeInHierarchy && obj.CompareTag(tag))
            {
                obj.SetActive(true);
                return obj;
            }
        }
        //Active when not enough objects in the pool
        return null ;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    public static ObjectPool operator +(ObjectPool poolA, ObjectPool poolB)
    {
        var rez = new ObjectPool(poolA.Pool.Count, poolA.Pool[0]);
        for (int i = 0;i < poolB.Pool.Count;i++)
        {
            rez.Pool.Add(poolB.Pool[i]);
        }
        return rez;
    }
}
