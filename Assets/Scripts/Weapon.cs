using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Attacker
{
    public int damage;
    public int cooldown;
    public float spreadAngle;

    [SerializeField]
    private Texture2D _crosshair;

    private bool _canShoot = true;

    public Action CurrentWeaponAttack;

    private void Awake()
    {
        Cursor.SetCursor(_crosshair, new Vector2(_crosshair.width / 2, _crosshair.height / 2), CursorMode.Auto);
    }

    private void Start()
    {
        if (Skills.Instance.skillList.Count <= 0)
        {
            CurrentWeaponAttack = Shoot;
        }
        else
        {
            Skills.Instance.RefreshSkills();
        }
        StartingFunction();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && _canShoot)
        {
            CurrentWeaponAttack();
            _canShoot = false;
            StartCoroutine(EnterCooldown());
        }
    }

    protected override void Shoot()
    {
        GameObject obj = gameObjectsPool.GetPooledObjectByTag("PlayerBullet");
           
        obj.transform.position = transform.position;
        Quaternion spreadRotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-spreadAngle / 2, spreadAngle / 2));
        var target = (spreadRotation * (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        obj.GetComponent<Bullet>().direction = target.normalized;
    }

    public void ShotgunShoot()
    {
        for (int i = 0; i < 6; ++i)
        {
            Shoot();
        }
    }

    private IEnumerator EnterCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        _canShoot = true;
    }
}
