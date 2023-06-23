using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingToPlayerEnemy : MovingEnemy
{
    private void Awake()
    {
        _minHeight = Camera.main.ScreenToWorldPoint(Vector2.zero);
        _maxHeight = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FireRateShoot());
        StartingFunction();
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
