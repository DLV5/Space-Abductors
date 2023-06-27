using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolWeapon : Weapon
{
    protected static ObjectPool _gameObjectsPool;

    [TagSelector, SerializeField] protected string _bulletTagToShoot;
    [SerializeField, Range(0, 360)] private float _spreadAngle;
    public float SpreadAngle { get; }

    public static PistolWeapon Instance { get; private set; }

    private PistolWeapon()
    {
        Instance = this;
    }

    private void Start()
    {
        _gameObjectsPool ??= PoolManager.BulletPool;
        InputHandler.PressingShootButton += InputHandler_OnPressingShootButton;
    }

    protected void InputHandler_OnPressingShootButton()
    {
        Fire();
    }
    protected override void Shoot()
    {
        var obj = _gameObjectsPool.GetPooledObjectByTag(_bulletTagToShoot);        
    }
}
