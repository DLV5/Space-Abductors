using UnityEngine;

public class EnemyBullet : Bullet
{
    private static Transform _playerTransform;

    protected override void OnEnable()
    {
        base.OnEnable();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;    
        CalculateDirection(_playerTransform.position);
    }

    protected void CalculateDirection(Vector3 targetPoint)
    {
        var target = (Vector2)(targetPoint - transform.position);
        Direction = target.normalized;
    }
}
