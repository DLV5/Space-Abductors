using System;
using System.Collections;
using UnityEngine;

public class Weapon : Attacker
{
    [Tooltip("Position of muzzle, it should be child of that gameobject")]
    [SerializeField] protected Transform _firePoint;

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
    public event Action Shooted;

    protected virtual void Awake()
    {
        Shooted += OnShooted;
    }

    private void Start()
    {
        //GameObject has only one child and it will be muzzle point
        _firePoint = gameObject.transform.GetChild(0);
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
        CanShoot = false;
        yield return new WaitForSeconds(1 / FireRate);
        CanShoot = true;
    }

    protected override void Fire()
    {
        if (CanShoot)
        {
            Shoot();
            Shooted?.Invoke();
        }
    }

    protected virtual void Shoot()
    {
        Debug.Log("Shoot");
        //position of shouted object should be set to the fire point
    }
}
