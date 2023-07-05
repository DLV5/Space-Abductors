using UnityEngine;

public class FlyingToOneDirectionEnemyBullet : EnemyBullet
{
    public static Vector3 TargetDirection;
    private Vector3 _localTargetDirection;

    protected override void OnEnable()
    {
        StartCoroutine(CheckIsInTheBoundOfTheScreen());
        _localTargetDirection = TargetDirection;
        CalculateDirection(_localTargetDirection);
    }
}
