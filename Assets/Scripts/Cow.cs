using UnityEngine;

public class Cow : MonoBehaviour
{
    public bool moving;
    public GameObject player;
    [SerializeField]
    private float speed = 1.0f;
    private EnemySpawner _spawner;

    private void Start()
    {
        _spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (moving)
        {
            var direction = player.transform.position - transform.position;
            transform.position += direction.normalized * speed * Time.deltaTime;
            if (direction.magnitude < 0.2f)
            {
                moving = false;
                _spawner.cowSpawned = false;
                _spawner.spawning = true;
            }
        }
    }
}
