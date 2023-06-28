using UnityEngine;

public class PlayerBullet : Bullet
{
    protected static Transform _playerTransform;
    protected float _spreadAngle = 0;

    protected virtual void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Initializate();
        CalculateDirection();
    }

    protected virtual void Initializate()
    {
        transform.position = _playerTransform.position;
    }
    protected void CalculateDirection()
    {
        Quaternion spreadRotation = Quaternion.Euler(0f, 0f, Random.Range(-_spreadAngle / 2, _spreadAngle / 2));
        var target = (spreadRotation * ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position));
        Direction = target.normalized;
    }
}
