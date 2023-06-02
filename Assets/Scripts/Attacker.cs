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

    protected static GameObjectsPool gameObjectsPool;
    void Start()
    {
        if (gameObjectsPool == null)
            gameObjectsPool = PoolManager.Instance.GetGameObjectsPool(bulletTag);
    }

    protected virtual void Shoot()
    {
        foreach (var obj in gameObjectsPool.pool)
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                obj.transform.position = transform.position;
                obj.GetComponent<Bullet>().direction = (target.transform.position - obj.transform.position).normalized;

                break;
            }
        }
    }
    protected virtual IEnumerator FireRateShoot()
    {
            yield return new WaitForSeconds(1 / fireRate);
            Shoot();
    }
}
