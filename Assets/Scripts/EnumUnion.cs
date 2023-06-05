using System;

[Serializable]
public class EnumUnion
{
    public Bullet.BulletTypes bulletType;
    public EnemySpawner.EnemyTypes enemyType;

    public AllEnumTypes typeToUse;
    public enum AllEnumTypes
    {
        EnemyTypes,
        BulletTypes,
    }
}