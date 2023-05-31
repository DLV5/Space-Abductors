using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunEnemy : Enemy
{
    [SerializeField]
    Transform target;

    public float spreadAngle;
    public int bulletsPerShot = 8;

    private static GameObjectsPool gameObjectsPool;

    private void Start()
    {
        if (gameObjectsPool == null)
            gameObjectsPool = GameManager.Instance.GetGameObjectsPool("BaseBullet");
        StartCoroutine(RepeatingShootAfterDelay());
    }

    void Shoot()
    {
        foreach (var obj in gameObjectsPool.pool)
        {
            for (int i = 0; i < bulletsPerShot; i++) 
            {
                if (!obj.activeSelf)
                {
                    obj.SetActive(true);
                    obj.transform.position = transform.position;
                    Quaternion spreadRotation = Quaternion.Euler(0f, 0f, Random.Range(-spreadAngle / 2, spreadAngle / 2));
                    obj.GetComponent<Bullet>().direction = (spreadRotation * (target.transform.position - obj.transform.position)).normalized;

                    break;
                }
            }
        }
    }

    private void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    IEnumerator RepeatingShootAfterDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / fireRate);
            Shoot();
        }
    }
}
