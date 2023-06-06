using System.Collections;
using UnityEngine;

public class ShotgunEnemy : MovingEnemy
{
    [Header("ShotGun Settings")]
    public float spreadAngle;
    public int bulletsPerShot = 8; 


    protected override void OnEnable()
    {
        base.OnEnable();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(FireRateShoot());
    }
    protected override void Shoot()
    {
        GameObject obj = gameObjectsPool.GetPooledObjectByTag("ShotGunBullet");

        obj.transform.position = transform.position;
        Quaternion spreadRotation = Quaternion.Euler(0f, 0f, Random.Range(-spreadAngle / 2, spreadAngle / 2));
        obj.GetComponent<Bullet>().direction = (spreadRotation * (target.transform.position - obj.transform.position)).normalized;
                  
    }   
    protected override IEnumerator FireRateShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / fireRate);
            for (int i = 0; i < bulletsPerShot; i++)
            {
                Shoot();
            }
        }
    }
}
