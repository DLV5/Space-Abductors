using UnityEngine;

public class Cow : MonoBehaviour
{
    public bool Moving;
    public GameObject Player;
    [SerializeField]
    private float speed = 1.0f;
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
            transform.position += direction.normalized * speed * Time.deltaTime;
            if (direction.magnitude < 0.2f)
            {
                Moving = false;
                _spawner.CowSpawned = false;
                _spawner.Spawning = true;
            }
        }
    }
}
