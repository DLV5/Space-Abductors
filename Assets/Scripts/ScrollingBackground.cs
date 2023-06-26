using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed;
    [SerializeField] private float _resetPointX;
    private Vector2 _startingPosition;

    private void Start()
    {
        _startingPosition = transform.position;
    }

    private void Update()
    {
        if (transform.position.x <= _resetPointX)
        {
            transform.position = _startingPosition;
        }
        transform.position -= new Vector3(_scrollSpeed * Time.timeScale * Time.deltaTime, 0f, 0f);
    }
}
