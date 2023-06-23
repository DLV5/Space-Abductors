using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingInOneDirectionEnemy : MovingEnemy
{
    [Header("Direction settings")]
    [SerializeField]
    [Tooltip("Direction value between 0 and 360. It should be divisible by 15")]
    protected float directionToShoot;

    protected Vector3 direction;

    private void Awake()
    {
        _minHeight = Camera.main.ScreenToWorldPoint(Vector2.zero);
        _maxHeight = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));

        float radians = directionToShoot * Mathf.Deg2Rad;

        // Calculate the direction vector
        direction = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians));

        // Normalize the direction vector
        direction.Normalize();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(FireRateShoot());
        StartingFunction();
    }

    protected override void Shoot()
    {
        GameObject obj = gameObjectsPool.GetPooledObjectByTag(bulletTagToShoot);

        obj.transform.position = transform.position;
        obj.GetComponent<Bullet>().direction = -direction;
    }
    protected override IEnumerator FireRateShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / fireRate);
            Shoot();
        }
    }

    private void OnDrawGizmosSelected()
    {
        float radians = directionToShoot * Mathf.Deg2Rad;

        // Calculate the direction vector
        direction = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians));

        // Normalize the direction vector
        direction.Normalize();

        // Set the color of the gizmo
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position, -direction * 10);
    }
}
