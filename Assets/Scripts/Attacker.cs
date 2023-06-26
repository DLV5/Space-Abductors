using System.Collections;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [TagSelector, SerializeField] protected string _bulletTagToShoot;
    protected GameObject _target;
    protected static ObjectPool gameObjectsPool;
    [SerializeField] protected float _fireRate = 1;

    public float FireRate 
    { 
        get => _fireRate;
        set => _fireRate = value; 
    }


    protected virtual void Start()
    {
        Initialize();
    }

    protected virtual void Initialize() {
        if (gameObjectsPool == null)
        {
            gameObjectsPool = PoolManager.BulletPool;
        }
    }
    protected virtual void Shoot()
    {
        var obj = gameObjectsPool.GetPooledObjectByTag(_bulletTagToShoot);
        
        obj.transform.position = transform.position;
        obj.GetComponent<Bullet>().Direction = (_target.transform.position - obj.transform.position).normalized;
    }
    protected virtual IEnumerator ShootAccordingToFireRate()
    {
        yield return new WaitForSeconds(1 / _fireRate);
        Shoot();
    }
}
