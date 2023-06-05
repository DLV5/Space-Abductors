using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private List<GameObject> pool;
    private GameObject prefab;
    public ObjectPool(int initialSize, GameObject prefab)
    {
        this.prefab = prefab;
        pool = new List<GameObject>();

        for (int i = 0; i < initialSize; i++)
        {
            CreatePooledObject();
        }
    }

    private GameObject CreatePooledObject()
    {
        GameObject obj = Object.Instantiate(prefab);
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in pool)
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
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy && obj.CompareTag(tag))
            {
                obj.SetActive(true);
                return obj;
            }
        }

        return CreatePooledObject();
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    public static ObjectPool operator +(ObjectPool poolA, ObjectPool poolB)
    {
        ObjectPool rez = new ObjectPool(poolA.pool.Count, poolA.pool[0]);
        for (int i = 0;i < poolB.pool.Count;i++)
        {
            rez.pool.Add(poolB.pool[i]);
        }
        return rez;
    }
}
