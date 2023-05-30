using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsPool
{
    public GameObject[] pool;
    public GameObjectsPool(int numberOfObjects, GameObject obj)
    {
        pool = new GameObject[numberOfObjects];
            
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = GameObject.Instantiate(obj, Vector3.zero, Quaternion.identity);
            pool[i].SetActive(false);
        }   
    }
}
