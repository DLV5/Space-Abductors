using UnityEngine;

public class BaseBullet : Bullet
{
    private void Update()
    {
        transform.position += speed * Direction * Time.deltaTime;
    }


}
