using UnityEngine;

public class EnemyBullet : Bullet
{
    private static Transform _playerTransform;

    protected override void OnEnable()
    {
        base.OnEnable();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;    
        CalculateDirection();
    }

    protected void CalculateDirection()
    {
        var target = (Vector2)(_playerTransform.position - transform.position);
        Direction = target.normalized;
    }
}
