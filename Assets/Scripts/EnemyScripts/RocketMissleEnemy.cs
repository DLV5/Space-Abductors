using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMissleEnemy : HelicopterEnemy
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void Shoot()
    {
        GameObject obj = gameObjectsPool.GetPooledObjectByTag("RocketMissle");
        obj.GetComponent<HomingBullet>().target = target;
        obj.transform.position = transform.position;
    }

    protected override IEnumerator FireRateShoot()
    {
        while(true)
        {
            yield return new WaitForSeconds(1 / fireRate);
            Shoot();
        }
    }
}
