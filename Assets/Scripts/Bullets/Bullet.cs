using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float _speed;
    public float Speed 
    { 
        get => _speed; 
        set => _speed = value; 
    }
    public Vector3 Direction { get; set;}

    private void FixedUpdate()
    {
        Move();
    }

    private void OnLevelWasLoaded(int level)
    {
        gameObject.SetActive(false);
    }
    protected virtual void OnEnable()
    {
         StartCoroutine(CheckIsInTheBoundOfTheScreen());       
    }

    protected virtual void Move()
    {
        transform.position += _speed * Time.deltaTime * Direction;
    }
    protected IEnumerator CheckIsInTheBoundOfTheScreen()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

            if (!Screen.safeArea.Contains(pos))
            {
                gameObject.SetActive(false);
            } 
        }
    }
}
