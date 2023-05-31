using UnityEngine;

public class Cow : MonoBehaviour
{
    public bool moving;
    public GameObject player;
    [SerializeField]
    private float speed = 1.0f;

    private void Update()
    {
        if (moving)
        {
            var direction = player.transform.position - transform.position;
            transform.position += direction.normalized * speed * Time.deltaTime;
            if (direction.magnitude < 0.2f)
            {
                moving = false;
            }
        }
    }
}
