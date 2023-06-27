using UnityEngine;

public class PlayerBullet : Bullet
{
    private static Transform _playerTransform;
    private float _spreadAngle;

    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Initializate();
        CalculateDirection();
    }

    private void Initializate()
    {
        _spreadAngle = PistolWeapon.Instance.SpreadAngle;
        transform.position = _playerTransform.position;
    }
    private void CalculateDirection()
    {
        Quaternion spreadRotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-_spreadAngle / 2, _spreadAngle / 2));
        var target = (spreadRotation * (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        Direction = target.normalized;
    }
}
