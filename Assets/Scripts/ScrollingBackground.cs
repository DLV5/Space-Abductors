using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed;
    [SerializeField]
    private float resetPointX;
    private Vector2 _startingPosition;

    private void Start()
    {
        _startingPosition = transform.position;
    }

    private void Update()
    {
        if (transform.position.x <= resetPointX)
        {
            transform.position = _startingPosition;
        }
        transform.position -= new Vector3(scrollSpeed * Time.timeScale * Time.deltaTime, 0f, 0f);
    }
}
