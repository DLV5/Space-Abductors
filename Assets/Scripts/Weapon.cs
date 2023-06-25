using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Weapon : Attacker
{
    [HideInInspector]
    public static Weapon Instance;

    public GameObject Flamethrower;
    public ParticleSystem Flames;
    public GameObject Railgun;
    public GameObject RailgunHolder;
    private Collider2D _flameCollider;
    private Animator animator;

    public int Damage;
    public float Cooldown = 1;
    public float SpreadAngle;
    public int BulletsPerShotgunShot = 6;
    public WeaponType Type;

    [SerializeField]
    private Texture2D _crosshair;
    [Header("Audio")]
    [SerializeField]
    public AudioClip RailgunShotSound;
    public AudioSource Source;


    private bool _canShoot = true;

    public Action CurrentWeaponAttack;

    public enum WeaponType
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
        animator = Railgun.GetComponent<Animator>();
        Source = GetComponent<AudioSource>();
        Cursor.SetCursor(_crosshair, new Vector2(_crosshair.width / 2, _crosshair.height / 2), CursorMode.Auto);
    }

    protected override void Start()
    {
        if (Skills.Instance.SkillList.Count <= 0)
        {
            CurrentWeaponAttack = Shoot;
        }
        else
        {
            Skills.Instance.RefreshSkills();
        }
        base.Start();
        _flameCollider = Flamethrower.GetComponent<Collider2D>();
    }

    private void Update()
    {
        switch (Type)
        {
            case WeaponType.ShootingWeapon:
                if (Input.GetKey(KeyCode.Mouse0) && _canShoot)
                {
                    CurrentWeaponAttack();
                    _canShoot = false;
                    StartCoroutine(EnterCooldown());
                }
                break;

            case WeaponType.ChargingWeapon:
                if (Input.GetKeyDown(KeyCode.Mouse0) && _canShoot)
                {
                    Railgun.SetActive(true);
                }
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    animator.SetTrigger("IsReleased");
                    Source.Play();
                    CurrentWeaponAttack();
                }
                break;
            case WeaponType.HoldingWeapon:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    CurrentWeaponAttack();
                }
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    _flameCollider.enabled = false;
                    Flames.Stop();
                }
                break;
            default: break;
        }
        
    }

    protected override void Shoot()
    {
        GameObject obj = gameObjectsPool.GetPooledObjectByTag("PlayerBullet");
           
        obj.transform.position = transform.position;
        Quaternion spreadRotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-SpreadAngle / 2, SpreadAngle / 2));
        var target = (spreadRotation * (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        obj.GetComponent<Bullet>().Direction = target.normalized;
    }

    public void ShotgunShoot()
    {
        for (int i = 0; i < BulletsPerShotgunShot; ++i)
        {
            Shoot();
        }
    }

    public void RailgunShoot()
    {
        Vector2 dir = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        dir = dir.normalized;
        //var hits = Physics2D.RaycastAll(transform.position, dir, 10f);
        float angle = Vector2.Angle(transform.position, dir);
        var hits = Physics2D.BoxCastAll(transform.position, new Vector2(0.1f, 1f), angle, dir, 30f);
        //BoxCastDebug.instance.StartDrawing(transform.position, dir, new Vector2(10f, 0.1f));
        foreach (var hit in hits)
        {
            Collider2D col = hit.collider;
            if (col == null) continue;
            if (col.CompareTag("ShotGunEnemy") || col.CompareTag("HelicopterEnemy") || col.CompareTag("BossEnemy") 
                || col.CompareTag("HealingEnemy"))
            {
                Enemy enemy = col.gameObject.GetComponent<Enemy>();
                enemy.Damage(Damage);
            }
        }
        Damage = 1;
    }

    public void FlamethrowerShoot()
    {
        Flames.Play();
        _flameCollider.enabled = true;
    }

    private IEnumerator EnterCooldown()
    {
        yield return new WaitForSeconds(Cooldown);
        _canShoot = true;
    }
}
