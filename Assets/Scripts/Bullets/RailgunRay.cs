using UnityEngine;

public class RailgunRay : MonoBehaviour
{
    public int Damage { get; set; }
    public float DamageMultiplier { get; set; }

    private RailgunWeapon _railgunWeapon;

    private void Start()
    {
        _railgunWeapon = FindObjectOfType<RailgunWeapon>();
        Damage = _railgunWeapon.Damage;
        DamageMultiplier = _railgunWeapon.DamageMultiplier;
    }

    public void CastRayThroughMousePosition()
    {
        var dir = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        dir = dir.normalized;
        
        float angle = Vector2.Angle(transform.position, dir);
        var hits = Physics2D.BoxCastAll(transform.position, new Vector2(0.1f, 1f), angle, dir, 30f);
        
        foreach (var hit in hits)
        {
            var col = hit.collider;
            if (col == null)
                continue;

            if (col.tag.Contains("Enemy"))
            {
                var enemy = col.gameObject.GetComponent<EnemyDamageHandler>();
                enemy?.Damage(Damage);
            }
        }
        Damage = _railgunWeapon.Damage;
    }

    public void MultiplyDamageOnCharge()
    {
        Damage = (int)(Damage * DamageMultiplier);
    }
}
