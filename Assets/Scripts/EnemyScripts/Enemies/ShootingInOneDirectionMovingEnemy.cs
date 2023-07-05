using System.Collections;
using UnityEngine;

public class ShootingInOneDirectionMovingEnemy : MovingEnemy
{
    [Header("Direction settings")]
    [Tooltip("Direction value between 0 and 360. It should be divisible by 15")]
    [SerializeField, Range(0,24)] protected int _directionToShoot;

    protected Vector3 _direction;

    private void Awake()
    {
        _minHeight = Camera.main.ScreenToWorldPoint(Vector2.zero);
        _maxHeight = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));

        //15f - multiplier for direction to shoot
        float radians = _directionToShoot * 15f * Mathf.Deg2Rad;

        // Calculate the direction vector
        _direction = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians));

        // Normalize the direction vector
        _direction.Normalize();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(ShootAccordingToFireRate());
    }

    protected override void Fire()
    {
        FlyingToOneDirectionEnemyBullet.TargetDirection = _direction;
        var obj = GameObjectsPool.GetPooledObjectByTag(_bulletTagToShoot);
        obj.transform.position = transform.position;
    }
    protected virtual IEnumerator ShootAccordingToFireRate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / _fireRate);
            Fire();
        }
    }

    private void OnDrawGizmosSelected()
    {
        //15f - multiplier for direction to shoot
        float radians = _directionToShoot * 15f * Mathf.Deg2Rad;

        // Calculate the direction vector
        _direction = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians));

        // Normalize the direction vector
        _direction.Normalize();

        // Set the color of the gizmo
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position, -_direction * 10);
    }
}
