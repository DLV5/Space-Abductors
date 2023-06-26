using UnityEngine;

public class CowStealing : MonoBehaviour
{
    private Ship _player; // For healing
    private Movement _movementScript; // For limiting movement
    private Cow _currentCow;

    private void Start()
    {
        _movementScript = GetComponent<Movement>();
        _player = GetComponent<Ship>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StealCow();
        }
        if (!_movementScript.CanMove)
        {
            if (!_currentCow.Moving)
            {
                Destroy(_currentCow.gameObject);
                Skills.Instance.AddSkillpoints(1);
                _player.Damage(-1);
                _movementScript.CanMove = true;
            }
        }
    }

    private void StealCow()
    {
        if (!_movementScript.CanMove) return;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        Collider2D cow = hit.collider;
        if (cow == null) return;
        if (cow.CompareTag("Cow"))
        {
            _currentCow = cow.gameObject.GetComponent<Cow>();
            _currentCow.Moving = true;
            _movementScript.CanMove = false;
        }
    }

}
