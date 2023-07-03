using UnityEngine;

public class Cow : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    private EnemySpawner _spawner;
    public bool IsMoving { get; set;}
    public GameObject Player { get; set;}

    private void Start()
    {
        _spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        Player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (IsMoving)
        {
            var direction = Player.transform.position - transform.position;
            transform.position += direction.normalized * _speed * Time.deltaTime;
            if (direction.magnitude < 0.2f)
            {
                IsMoving = false;
                _spawner.HasCowSpawned = false;
                _spawner.IsSpawning = true;
            }
        }
    }
}
