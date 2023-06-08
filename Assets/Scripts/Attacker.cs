using System.Collections;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField]
    protected string bulletTag = "PlayerBullet";

    protected Transform target;

    [SerializeField]
    protected float fireRate = 1;

    public float FireRate { get => fireRate; set => fireRate = value; }

    protected static ObjectPool gameObjectsPool;

    protected void Start()
    {
        StartingFunction();
    }

    protected virtual void StartingFunction() {
        if (gameObjectsPool == null)
            gameObjectsPool = PoolManager.bulletPool;
    }
    protected virtual void Shoot()
    {
        GameObject obj = gameObjectsPool.GetPooledObjectByTag("BaseBullet");
        
        obj.transform.position = transform.position;
        obj.GetComponent<Bullet>().direction = (target.transform.position - obj.transform.position).normalized;
    }
    protected virtual IEnumerator FireRateShoot()
    {
            yield return new WaitForSeconds(1 / fireRate);
            Shoot();
    }
}
