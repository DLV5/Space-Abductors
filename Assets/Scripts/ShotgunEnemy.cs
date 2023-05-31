using System.Collections;
using UnityEngine;

public class ShotgunEnemy : MovingEnemy
{
    [Header("ShotGun Settings")]
    public float spreadAngle;
    public int bulletsPerShot = 8; 


    private void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    protected override void Shoot()
    {
        foreach (var obj in gameObjectsPool.pool)
        {          
                if (!obj.activeSelf)
                {
                    obj.SetActive(true);
                    obj.transform.position = transform.position;
                    Quaternion spreadRotation = Quaternion.Euler(0f, 0f, Random.Range(-spreadAngle / 2, spreadAngle / 2));
                    obj.GetComponent<Bullet>().direction = (spreadRotation * (target.transform.position - obj.transform.position)).normalized;
                    break;
                } 
                
        }
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
