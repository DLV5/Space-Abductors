using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : Bullet
{
    public float rotateSpeed = 200f;
    public GameObject target;
    public Rigidbody2D rb;

    private void Start()
    {
        //GetPlayers pos
    }
    void Update()
    {
        Vector2 dir = ((Vector2)target.transform.position - (Vector2)transform.position).normalized;

        float rotationAmount = Vector3.Cross(dir, transform.up).z;
        rb.angularVelocity = -rotationAmount * rotateSpeed;
        rb.velocity = transform.up * speed;
    }
}
