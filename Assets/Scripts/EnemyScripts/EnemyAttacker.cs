using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(EnemyUI))]
[RequireComponent(typeof(EnemyDamageHanlder))]
public class EnemyAttacker : Attacker
{
    [TagSelector, SerializeField] protected string _bulletTagToShoot;
    protected EnemyBehavior currentState = EnemyBehavior.FlyingToTheScreen;
    protected static ObjectsPool GameObjectsPool
    {
        get => PoolManager.BulletPool;
    }

    protected GameObject _target;
    
    public override float FireRate 
    { 
        get => _fireRate; 
        set => _fireRate = value; 
    }

    protected enum EnemyBehavior
    {
        FlyingToTheScreen = 0,
        Attacking = 1,
        Leaving = 2
    }

    protected virtual void Awake()
    {
        SetFirePoint();
    }

    protected override void Fire()
    {
        var bullet = GameObjectsPool.GetPooledObjectByTag(_bulletTagToShoot, false);
        bullet.transform.position = _firePoint.position;
        bullet.SetActive(true);
    }

    protected override void SetFirePoint()
    {
        _firePoint = transform.GetChild(0);
    }
}
