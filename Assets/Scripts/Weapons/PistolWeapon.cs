using UnityEngine;

public class PistolWeapon : Weapon
{
    protected static ObjectsPool _gameObjectsPool;

    [TagSelector, SerializeField] protected string _bulletTagToShoot;
    [SerializeField, Range(0, 360)] protected float _spreadAngle;
    public float SpreadAngle 
    { 
        get => _spreadAngle; 
    }

    public static PistolWeapon Instance { get; protected set; }

    protected PistolWeapon()
    {
        Instance = this;
    }

    protected override void Awake()
    {
        base.Awake();
        //GameObject has only one child and it will be muzzle point
        _firePoint = gameObject.transform.GetChild(0);
        PlayerBullet.FirePoint = _firePoint;
    }

    protected void Start()
    {
        _gameObjectsPool ??= PoolManager.BulletPool;
        InputHandler.PressingShootButton += InputHandler_OnPressingShootButton;
    }

    protected virtual void InputHandler_OnPressingShootButton()
    {
        Fire();
    }
    protected override void Shoot()
    {
        //Enabeling object, all other stuff bullet handle by itself
        _gameObjectsPool.GetPooledObjectByTag(_bulletTagToShoot);
    }
}
