using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : Bullet
{
    private void Update()
    {
        transform.position += speed * direction * Time.deltaTime;
    }


}
