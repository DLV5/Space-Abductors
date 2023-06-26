using System.Collections;
using UnityEngine;

public class ShotgunShootingInOneDirectionMovingEnemy : ShootingInOneDirectionMovingEnemy
{
    [Header("ShotGun Settings")]
    public float SpreadAngle;
    public int BulletsPerShot = 8;


    protected override void OnEnable()
    {
        base.OnEnable();
        _target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ShootAccordingToFireRate());
        Initialize();
    }
    protected override void Shoot()
    {
        GameObject obj = gameObjectsPool?.GetPooledObjectByTag("ShotGunBullet");
        obj.transform.position = transform.position ;
        Quaternion spreadRotation = Quaternion.Euler(0f, 0f, Random.Range(-SpreadAngle / 2, SpreadAngle / 2));
        obj.GetComponent<Bullet>().Direction = (spreadRotation * -direction).normalized;

    }
    protected override IEnumerator ShootAccordingToFireRate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / _fireRate);
            for (int i = 0; i < BulletsPerShot; i++)
            {
                Shoot();
            }
        }
    }
}
