using System.Collections;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [TagSelector, SerializeField]
    protected string bulletTagToShoot;

    protected GameObject target;

    [SerializeField]
    protected float fireRate = 1;

    public float FireRate { get => fireRate; set => fireRate = value; }

    protected static ObjectPool gameObjectsPool;

    protected virtual void Start()
    {
        Initialize();
    }

    protected virtual void Initialize() {
        if (gameObjectsPool == null)
            gameObjectsPool = PoolManager.bulletPool;
    }
    protected virtual void Shoot()
    {
        GameObject obj = gameObjectsPool.GetPooledObjectByTag(bulletTagToShoot);
        
        obj.transform.position = transform.position;
        obj.GetComponent<Bullet>().Direction = (target.transform.position - obj.transform.position).normalized;
    }
    protected virtual IEnumerator ShootAccordingToFireRate()
    {
            yield return new WaitForSeconds(1 / fireRate);
            Shoot();
    }
}
