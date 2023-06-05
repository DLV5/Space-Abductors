using UnityEngine;
using TMPro;

public class CowStealing : MonoBehaviour
{
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
                Skills.Instance.AddSkillpoints(1);
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
            _currentCow = cow.gameObject.GetComponent<Cow>();
            _currentCow.moving = true;
            _movementScript.canMove = false;
        }
    }

    
}
