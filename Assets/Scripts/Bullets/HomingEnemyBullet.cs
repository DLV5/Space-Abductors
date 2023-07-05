using UnityEngine;

public class HomingEnemyBullet : Bullet
{
    public float RotateSpeed { get; set;} = 200f;
    public GameObject Target { get; set; }
    public Rigidbody2D Rb { get; set; }

    private void Start()
    {
        //Target = Weapon.Instance.gameObject;
    }
    void Update()
    {
        Vector2 dir = ((Vector2)Target.transform.position - (Vector2)transform.position).normalized;

        float rotationAmount = Vector3.Cross(dir, transform.up).z;
        Rb.angularVelocity = -rotationAmount * RotateSpeed;
        Rb.velocity = transform.up * _speed;
    }
}
