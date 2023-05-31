using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class HelicopterEnemy : MovingEnemy
{

    private void Awake()
    {
        _minHeight = Camera.main.ScreenToWorldPoint(Vector2.zero);
        _maxHeight = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
    }
    private void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
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
