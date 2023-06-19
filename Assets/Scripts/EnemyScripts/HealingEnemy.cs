using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HealingEnemy : HelicopterEnemy
{
    private Enemy[] targets = new Enemy[]{};

    protected override void Awake()
    {
        base.Awake();
        targets = GameObject.FindObjectsOfType<Enemy>(false);
    }
    protected override void OnEnable()
    {
        base.OnEnable(); 

    }
        protected override IEnumerator FireRateShoot()
    {
        while (true)
        {
            ChoseRandomEnemy();
            yield return new WaitForSeconds(1 / fireRate);
            Shoot();
        }
    }
    protected override void Shoot()
    {
        GameObject obj = gameObjectsPool.GetPooledObjectByTag("HealingBullet");

        obj.transform.position = transform.position;
        obj.GetComponent<Bullet>().direction = (target.transform.position - obj.transform.position).normalized;
    }
    private void ChoseRandomEnemy()
    {
        int rand = UnityEngine.Random.Range(0, targets.Length);
        target = targets[rand].gameObject.transform;
    }
}

