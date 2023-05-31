using UnityEngine;

public class Skillpoints : MonoBehaviour
{
    public static int skillPoints = 0;
    private Movement _movementScript; // For limiting movement
    private Cow _currentCow;

    private void Start()
    {
        _movementScript = GetComponent<Movement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StealCow();
        }
        if (!_movementScript.canMove)
        {
            if (!_currentCow.moving)
            {
                _currentCow.gameObject.SetActive(false);
                ++skillPoints;
                _movementScript.canMove = true;
            }
        }
    }

    private void StealCow()
    {
        if (!_movementScript.canMove) return;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        Collider2D cow = hit.collider;
        if (cow == null) return;
        if (cow.CompareTag("Cow"))
        {
            Debug.Log("CompareTag");
            _currentCow = cow.gameObject.GetComponent<Cow>();
            _currentCow.moving = true;
            _movementScript.canMove = false;
        }
    }
}
