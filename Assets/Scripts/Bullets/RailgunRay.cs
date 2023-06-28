using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunRay : MonoBehaviour
{
    [SerializeField] int _damage;

    public void CastRayThroughMouseBosition()
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
                var enemy = col.gameObject.GetComponent<EnemyAttacker>();
                enemy.Damage(_damage);
            }
        }
        _damage = 1;
    }

    public void AddDamageOnCharge(int damageToAdd)
    {
        _damage += damageToAdd;
    }
}
