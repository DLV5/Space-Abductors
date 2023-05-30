using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterEnemy : Enemy
{
    [SerializeField]
    private Transform target;    

    private static GameObjectsPool gameObjectsPool;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObjectsPool == null)
            gameObjectsPool = GameManager.Instance.GetGameObjectsPool(bulletPrefab);

        StartCoroutine(RepeatingShootAfrterDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot()
    {       
        foreach(var gameObject in gameObjectsPool.pool) {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                gameObject.transform.position = transform.position;
                gameObject.GetComponent<Bullet>().direction = (target.transform.position - gameObject.transform.position).normalized;

                break;
            }
        }
    }

    IEnumerator RepeatingShootAfrterDelay()
    {
        while(true)
        {
            yield return new WaitForSeconds(1 / fireRate);
            Shoot();
        }
    }
}
