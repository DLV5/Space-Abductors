using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public bool CanMove { get; set; } = true;

    [SerializeField] private float _speed = 5.0f;
    private Vector2 _direction;

    private Vector2 _minScreenBounds;
    private Vector2 _maxScreenBounds;

    private void Awake()
    {
        _minScreenBounds = Camera.main.ScreenToWorldPoint(Vector2.zero);
        _maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        CowStealing.StartedCowCapture += ToggleCanMove;
        CowStealing.FinishedCowCapture += ToggleCanMove;
    }

    private void OnDisable()
    {
        CowStealing.StartedCowCapture -= ToggleCanMove;
        CowStealing.FinishedCowCapture -= ToggleCanMove;
    }

    private void FixedUpdate()
    {
        if (CanMove)
        {            
            transform.position += (Vector3)_direction * _speed * Time.deltaTime;
        }

        float x = transform.position.x;
        float y = transform.position.y;

        // Most sane unity code
        if (x > _maxScreenBounds.x)
        {
            transform.position = new Vector2(_maxScreenBounds.x, transform.position.y);
        }
        if (y > _maxScreenBounds.y)
        {
            transform.position = new Vector2(transform.position.x, _maxScreenBounds.y);
        }
        if (x < _minScreenBounds.x)
        {
            transform.position = new Vector2(_minScreenBounds.x, transform.position.y);
        }
        if (y < _minScreenBounds.y)
        {
            transform.position = new Vector2(transform.position.x, _minScreenBounds.y);
        }
    }

    public void ToggleCanMove()
    {
        CanMove = !CanMove;
    }

    private void OnMove(InputValue inputValue) {
        _direction = inputValue.Get<Vector2>();
    }

}
