using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsPool : MonoBehaviour
{
    public GameObject[] gameObjectsPool;
    public GameObjectsPool(int numberOfObjects, GameObject obj)
    {
        gameObjectsPool = new GameObject[numberOfObjects];
        for (int i = 0; i < gameObjectsPool.Length; i++)
        {
            gameObjectsPool[i] = Instantiate(obj, Vector3.zero, Quaternion.identity);
            gameObjectsPool[i].SetActive(false);
        }   
    }
}
