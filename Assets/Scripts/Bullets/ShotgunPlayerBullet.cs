public class ShotgunPlayerBullet : PlayerBullet
{
    protected override void Initializate()
    {
        base.Initializate();
        _spreadAngle = ShotgunWeapon.ShotgunInstance.SpreadAngle;
    }
}
