using System.Collections;
using System.Collections.Generic;
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
        transform.position -= new Vector3(scrollSpeed * Time.timeScale * 0.01f, 0f, 0f);
    }
}
