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

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    protected void Start()
    {
        InputHandler.PressingShootButton += InputHandler_OnPressingShootButton;
        _gameObjectsPool ??= PoolManager.BulletPool;
    }

    protected override void Initialize()
    {
        base.Initialize();
        _firePoint = gameObject.transform.GetChild(0);
        PlayerBullet.FirePoint = _firePoint;
    }

    protected override void Uninitialize()
    {
        base.Uninitialize();
        InputHandler.PressingShootButton -= InputHandler_OnPressingShootButton;
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
