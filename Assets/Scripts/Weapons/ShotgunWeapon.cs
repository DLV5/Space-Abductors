using UnityEngine;

public class ShotgunWeapon : PistolWeapon
{
    [SerializeField] private int _bulletsPerShotgunShot = 6;

    public static ShotgunWeapon ShotgunInstance { get; protected set; }

    protected override void Awake()
    {
        if (ShotgunInstance != null && ShotgunInstance != this)
        {
            Destroy(Instance);
        }
        else
        {
            ShotgunInstance = this;
        }
    }

    protected override void Shoot()
    {
        for (int i = 0; i < _bulletsPerShotgunShot; i++)
        {
            _gameObjectsPool.GetPooledObjectByTag(_bulletTagToShoot);
        }
    }
}
