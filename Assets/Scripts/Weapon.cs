using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Attacker
{
    [SerializeField]
    private Texture2D _crosshair;


    private void Awake()
    {
        Cursor.SetCursor(_crosshair, new Vector2(_crosshair.width / 2, _crosshair.height / 2), CursorMode.Auto);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && _canShoot)
        {
            Shoot();
        }
    }
}
