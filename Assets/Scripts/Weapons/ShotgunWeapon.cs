using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : PistolWeapon
{
    [SerializeField] private int _bulletsPerShotgunShot = 6;

    public static ShotgunWeapon ShotgunInstance { get; protected set; }
    private ShotgunWeapon()
    {
        ShotgunInstance = this;
    }

    protected override void Shoot()
    {
        for (int i = 0; i < _bulletsPerShotgunShot; i++)
        {
            _gameObjectsPool.GetPooledObjectByTag(_bulletTagToShoot);
        }
    }
}
