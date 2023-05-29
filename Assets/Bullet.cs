using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    public float Speed { get => speed; set => speed = value; }

    [SerializeField]
    protected int damage;
    public int Damage { get => damage; set => damage = value; }

    protected Vector3 direction;
}
