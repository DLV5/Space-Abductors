using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    private void Update()
    {
        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }
}
