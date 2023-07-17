using System.Collections;
using UnityEngine;

public class HomingEnemyBullet : Bullet
{
    public float RotateSpeed { get; set;} = 200f;
    public static GameObject Target { get; set; }
    public Rigidbody2D Rb { get; set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        Rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DisableAfterFewSeconds());
        Target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector2 dir = ((Vector2)Target.transform.position - (Vector2)transform.position).normalized;

        float rotationAmount = Vector3.Cross(dir, transform.up).z;
        Rb.angularVelocity = -rotationAmount * RotateSpeed;
        Rb.velocity = transform.up * _speed;
    }

    private IEnumerator DisableAfterFewSeconds()
    {
        yield return new WaitForSeconds(3);

        gameObject.SetActive(false);
    }
}
