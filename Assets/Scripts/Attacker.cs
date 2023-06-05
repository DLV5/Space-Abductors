using System.Collections;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField]
    protected string bulletTag;

    protected Transform target;

    [SerializeField]
    protected float fireRate = 1;

    public float FireRate { get => fireRate; set => fireRate = value; }

    protected static ObjectPool gameObjectsPool;

    void Start()
    {
        if (gameObjectsPool == null)
            gameObjectsPool = PoolManager.Instance.bulletPool;
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
