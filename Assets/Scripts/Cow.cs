using UnityEngine;

public class Cow : MonoBehaviour
{
    public bool Moving;
    public GameObject Player;
    [SerializeField] private float _speed = 1.0f;
    private EnemySpawner _spawner;

    private void Start()
    {
        _spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        Player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (Moving)
        {
            var direction = Player.transform.position - transform.position;
            transform.position += direction.normalized * _speed * Time.deltaTime;
            if (direction.magnitude < 0.2f)
            {
                Moving = false;
                _spawner.HasCowSpawned = false;
                _spawner.IsSpawning = true;
            }
        }
    }
}
