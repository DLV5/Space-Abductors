using UnityEngine;

public class PlayerBullet : Bullet
{
    protected static Transform _firePoint;
    public static Transform FirePoint
    {
        get => _firePoint;
        set => _firePoint = value;
    }

    protected float _spreadAngle = 0;

    protected override void OnEnable()
    {
        base.OnEnable();
        Initializate();
        CalculateDirection();
    }

    protected virtual void Initializate()
    {
        transform.position = _firePoint.position;
    }
    protected void CalculateDirection()
    {
        Quaternion spreadRotation = Quaternion.Euler(0f, 0f, Random.Range(-_spreadAngle / 2, _spreadAngle / 2));
        var target = (spreadRotation * ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position));
        Direction = target.normalized;
    }
}
