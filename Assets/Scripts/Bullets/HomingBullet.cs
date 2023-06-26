using UnityEngine;

public class HomingBullet : Bullet
{
    public float RotateSpeed = 200f;
    public GameObject Target;
    public Rigidbody2D Rb;

    private void Start()
    {
        Target = Weapon.Instance.gameObject;
    }
    void Update()
    {
        Vector2 dir = ((Vector2)Target.transform.position - (Vector2)transform.position).normalized;

        float rotationAmount = Vector3.Cross(dir, transform.up).z;
        Rb.angularVelocity = -rotationAmount * RotateSpeed;
        Rb.velocity = transform.up * _speed;
    }
}
