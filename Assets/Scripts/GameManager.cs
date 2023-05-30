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
    void Awake()
    {
       for (int i = 0; i < bulletsPrefabs.Count; i++) {
        GameObjectsPool gameObjectsPool = new GameObjectsPool(20, bulletsPrefabs[i]);
        gameObjectsPools.Add(gameObjectsPool);
        }
    }

    public GameObjectsPool GetGameObjectsPool(string bulletTag)
    {
        for (int i = 0; i < gameObjectsPools.Count; i++)
        {
            if(bulletTag == gameObjectsPools[i].pool[0].tag) return gameObjectsPools[i];
        }

        throw new System.IndexOutOfRangeException();
    }


}
