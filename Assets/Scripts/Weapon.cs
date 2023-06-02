using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;
    public int cooldown;
    public float spreadAngle;

    [SerializeField]
    private Texture2D _crosshair;

    private static GameObjectsPool gameObjectsPool;
    private bool _canShoot = true;

    private void Awake()
    {
        Cursor.SetCursor(_crosshair, new Vector2(_crosshair.width / 2, _crosshair.height / 2), CursorMode.Auto);
    }

    private void Start()
    {
        if (gameObjectsPool == null)
            gameObjectsPool = PoolManager.Instance.GetGameObjectsPool("PlayerBullet");
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && _canShoot)
        {
            Shoot();
            _canShoot = false;
            StartCoroutine(EnterCooldown());
        }
    }

    public void Shoot()
    {
        foreach (var obj in gameObjectsPool.pool)
        {
            if (!obj.activeSelf)
            {
           
                obj.SetActive(true);
                obj.transform.position = transform.position;
                Quaternion spreadRotation = Quaternion.Euler(0f, 0f, Random.Range(-spreadAngle / 2, spreadAngle / 2));
                var target = (spreadRotation * (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
                obj.GetComponent<Bullet>().direction = target.normalized;

                break;
            }
        }
    }

    private IEnumerator EnterCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        _canShoot = true;
    }
}
