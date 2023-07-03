public class PistolPlayerBullet : PlayerBullet
{
    protected override void Initializate()
    {
        base.Initializate();
        _spreadAngle = PistolWeapon.Instance.SpreadAngle;
    }
}
