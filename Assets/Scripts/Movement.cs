using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public bool CanMove = true;

    [SerializeField]
    private float speed = 5.0f;
    private Vector2 _direction;

    private Vector2 _minScreenBounds;
    private Vector2 _maxScreenBounds;

    private void Awake()
    {
        _minScreenBounds = Camera.main.ScreenToWorldPoint(Vector2.zero);
        _maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    private void Update()
    {
        if (CanMove)
        {
            _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            transform.position += (Vector3)_direction * speed * Time.deltaTime;
        }

        // Most sane unity code
        if (transform.position.x > _maxScreenBounds.x)
            transform.position = new Vector2(_maxScreenBounds.x, transform.position.y);
        if (transform.position.y > _maxScreenBounds.y)
            transform.position = new Vector2(transform.position.x, _maxScreenBounds.y);
        if (transform.position.x < _minScreenBounds.x)
            transform.position = new Vector2(_minScreenBounds.x, transform.position.y);
        if (transform.position.y < _minScreenBounds.y)
            transform.position = new Vector2(transform.position.x, _minScreenBounds.y);
    }
}
