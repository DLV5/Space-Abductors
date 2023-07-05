using System;
using System.Collections;
using UnityEngine;

public class Weapon : Attacker
{
    [SerializeField] protected int _damage;
    public int Damage
    {
        get => _damage;
        set => _damage = value;
    }


    public override float FireRate 
    { 
        get => _fireRate;
        set => _fireRate = value;
    }

    //Move to UI
    //[SerializeField] private Texture2D _crosshair;

    //Move to audio
    //[Header("Audio")]
    //public AudioClip RailgunShotSound;
    //public AudioClip FlamethrowerSound;
    //public AudioClip PistolShotSound;
    //public AudioClip ShotgunSound;
    //public AudioSource Source;

    private bool _canShoot = true;
    public bool CanShoot 
    { 
        get => _canShoot; 
        set => _canShoot = value;
    }

    //Move to the next scripts
    //public Action CurrentWeaponAttack;
    public static event Action Shooted;

    protected virtual void OnEnable()
    {
        Debug.Log("WeaponOnEnableCallded");
        Initialize();
    }

    protected virtual void OnDisable()
    {
        Debug.Log("WeaponOnDisableCallded");
        Uninitialize();
    }

    protected virtual void Initialize()
    {
        SetFirePoint();
        Shooted += OnShooted;
    }
    protected virtual void Uninitialize()
    {
        Shooted -= OnShooted;
    }

    private void OnShooted()
    {
        EnterCooldown();
    }
    protected void EnterCooldown()
    {
        StartCoroutine(WaitBeforeNextShoot());
    }
    protected IEnumerator WaitBeforeNextShoot()
    {
        Debug.Log("CoroutineStarted");
        CanShoot = false;
        yield return new WaitForSeconds(1 / FireRate);
        CanShoot = true;
    }

    protected override void SetFirePoint()
    {
        _firePoint = transform.GetChild(0);
    }

    protected override void Fire()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing)
            return;
        if (!CanShoot) //&& GameManager.Instance.CurrentState == GameState.Playing)
            return;
            Shoot();
            Shooted?.Invoke();
    }

    protected virtual void Shoot()
    {
        Debug.Log("Shoot");
        //position of shouted object should be set to the fire point
    }
}
