using UnityEngine;

public class BaseBullet : Bullet
{
    private void Update()
    {
        transform.position += _speed * Direction * Time.deltaTime;
    }


}
