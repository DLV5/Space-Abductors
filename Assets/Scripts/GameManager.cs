using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    GameManager() { 
        Instance = this;
    }
    [SerializeField]
    List<GameObject> bulletsPrefabs = new List<GameObject> ();

    List<GameObjectsPool> gameObjectsPools = new List<GameObjectsPool>();
    void Start()
    {
       for (int i = 0; i < bulletsPrefabs.Count; i++) {
        GameObjectsPool gameObjectsPool = new GameObjectsPool(1, bulletsPrefabs[i]);
        gameObjectsPools.Add(gameObjectsPool);
        }
    }


}
