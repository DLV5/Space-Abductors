using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railgun : MonoBehaviour
{
    private void Update()
    {
        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
}
