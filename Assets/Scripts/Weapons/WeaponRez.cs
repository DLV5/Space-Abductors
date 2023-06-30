using System;
using System.Collections;
using UnityEngine;

public class WeaponRez : Attacker
{
    public static WeaponRez Instance { get; set; }

    [Header("Weapon Objects")]
    public GameObject Flamethrower;
    public ParticleSystem Flames;
    public GameObject Railgun;
    public GameObject RailgunHolder;
    private Collider2D _flameCollider;
    private Animator _animator;

    [Header("Stats")]
    public int Damage;
    public float Cooldown = 1;
    public float SpreadAngle;
    public int BulletsPerShotgunShot = 6;
    public Type CurrentType;

    [SerializeField] private Texture2D _crosshair;

    [Header("Audio")]
    public AudioClip RailgunShotSound;
    public AudioClip FlamethrowerSound;
    public AudioClip PistolShotSound;
    public AudioClip ShotgunSound;
    public AudioSource Source;

    private bool _canShoot = true;
    public bool CanShoot 
    { 
        get => _canShoot; 
        set => _canShoot = value; 
    }
    public override float FireRate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Action CurrentWeaponAttack;

    public enum Type
    {
        ChargingWeapon,
        ShootingWeapon,
        HoldingWeapon,
    }

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
        _animator = Railgun.GetComponent<Animator>();
        Source = GetComponent<AudioSource>();
        Source.clip = PistolShotSound;
        Cursor.SetCursor(_crosshair, new Vector2(_crosshair.width / 2, _crosshair.height / 2), CursorMode.Auto);
    }

    protected virtual void Start()
    {
        if (Skills.Instance.SkillList.Count <= 0)
        {
            CurrentWeaponAttack = Fire;
        }
        else
        {
            Skills.Instance.RefreshSkills();
        }
        //base.Start();
        _flameCollider = Flamethrower.GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        InputHandler.PressingShootButton += InputHandler_PressingShootButton;
        InputHandler.HoldingShootButton += InputHandler_HoldingShootButton;
        InputHandler.ReleasingShootButton += InputHandler_ReleasingShootButton;
    }

    private void OnDisable()
    {
        InputHandler.PressingShootButton -= InputHandler_PressingShootButton;
        InputHandler.HoldingShootButton -= InputHandler_HoldingShootButton;
        InputHandler.ReleasingShootButton -= InputHandler_ReleasingShootButton;
    }

    private void Update()
    {
        //switch (CurrentType)
        //{
        //    case Type.ShootingWeapon:
        //        if (Input.GetKey(KeyCode.Mouse0) && _canShoot)
        //        {
        //            Source.Play();
        //            CurrentWeaponAttack();
        //            CanShoot = false;
        //            StartCoroutine(EnterCooldown());
        //        }
        //        break;

        //    case Type.ChargingWeapon:
        //        if (Input.GetKeyDown(KeyCode.Mouse0) && _canShoot)
        //        {
        //            Railgun.SetActive(true);
        //        }
        //        if (Input.GetKeyUp(KeyCode.Mouse0))
        //        {
        //            _animator.SetTrigger("IsReleased");
        //            Source.Play();
        //            CurrentWeaponAttack();
        //        }
        //        break;
        //    case Type.HoldingWeapon:
        //        if (Input.GetKeyDown(KeyCode.Mouse0))
        //        {
        //            if (!Source.isPlaying)
        //            {
        //                Source.Play();
        //            }
        //            CurrentWeaponAttack();
        //        }
        //        if (Input.GetKeyUp(KeyCode.Mouse0))
        //        {
        //            Source.Stop();
        //            _flameCollider.enabled = false;
        //            Flames.Stop();
        //        }
        //        break;
        //    default: break;
        //}
        
    }

    //public void ShootLikeShootgun()
    //{
    //    for (int i = 0; i < BulletsPerShotgunShot; ++i)
    //    {
    //        Fire();
    //    }
    //}

    //public void ShootLikeRailgun()
    //{
    //    var dir = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
    //    dir = dir.normalized;
    //    //var hits = Physics2D.RaycastAll(transform.position, dir, 10f);
    //    float angle = Vector2.Angle(transform.position, dir);
    //    var hits = Physics2D.BoxCastAll(transform.position, new Vector2(0.1f, 1f), angle, dir, 30f);
    //    //BoxCastDebug.instance.StartDrawing(transform.position, dir, new Vector2(10f, 0.1f));
    //    foreach (var hit in hits)
    //    {
    //        var col = hit.collider;
    //        if (col == null) 
    //            continue;
    //        if (col.CompareTag("ShotGunEnemy") || col.CompareTag("HelicopterEnemy") || col.CompareTag("BossEnemy") 
    //            || col.CompareTag("HealingEnemy"))
    //        {
    //            var enemy = col.gameObject.GetComponent<EnemyAttacker>();
    //            enemy.Damage(Damage);
    //        }
    //    }
    //    Damage = 1;
    //}

    //fire more suitable here
    public void ShootLikeFlamethrower()
    {
        Flames.Play();
        _flameCollider.enabled = true;
    }

    public IEnumerator EnterCooldown()
    {
        CanShoot = false;
        yield return new WaitForSeconds(Cooldown);
        CanShoot = true;
    }

    protected override void Fire()
    {
        //var obj = gameObjectsPool.GetPooledObjectByTag("PlayerBullet");
        //obj.transform.position = transform.position;
        //var spreadRotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-SpreadAngle / 2, SpreadAngle / 2));
        //var target = (spreadRotation * (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        //obj.GetComponent<Bullet>().Direction = target.normalized;
    }

    private void InputHandler_PressingShootButton()
    {
        if (CurrentType != Type.ShootingWeapon || !_canShoot)
            return;

        Source.Play();
        CurrentWeaponAttack();
        StartCoroutine(EnterCooldown());
        
    }

    private void InputHandler_HoldingShootButton()
    {
        if (CurrentType != Type.ChargingWeapon || !_canShoot)
            return;

        Debug.Log("Railgun holded");
        Railgun.SetActive(true);
        StartCoroutine(EnterCooldown());
    }

    private void InputHandler_ReleasingShootButton()
    {
        switch (CurrentType)
        {
            case Type.ShootingWeapon:
                break;
            case Type.ChargingWeapon:
                Debug.Log("Railgun released");
                _animator.SetTrigger("IsReleased");
                Source.Play();
                CurrentWeaponAttack();
                break;
            case Type.HoldingWeapon:
                Source.Stop();
                _flameCollider.enabled = false;
                Flames.Stop();
                break;
        }            
    }

}
