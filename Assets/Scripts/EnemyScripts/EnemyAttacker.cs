using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyUI))]
[RequireComponent(typeof(EnemyDamageHanlder))]
public class EnemyAttacker : Attacker
{
    [TagSelector, SerializeField] protected string _bulletTagToShoot;
    protected EnemyBehavior currentState = EnemyBehavior.FlyingToTheScreen;
    protected static ObjectPool gameObjectsPool;

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

    protected override void Fire()
    {
        var obj = gameObjectsPool.GetPooledObjectByTag(_bulletTagToShoot);

        obj.transform.position = transform.position;
        obj.GetComponent<Bullet>().Direction = (_target.transform.position - obj.transform.position).normalized;
    }
}
