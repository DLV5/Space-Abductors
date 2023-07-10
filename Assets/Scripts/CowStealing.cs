using UnityEngine;

public class CowStealing : MonoBehaviour
{
    private PlayerDamageHandler _player; // For healing
    private Movement _movementScript; // For limiting movement
    private Cow _currentCow;

    private void Start()
    {
        _movementScript = GetComponent<Movement>();
        _player = GetComponent<PlayerDamageHandler>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StealCow();
        }
        if (!_movementScript.CanMove)
        {
            if (!_currentCow.IsMoving)
            {
                Destroy(_currentCow.gameObject);
                Skills.Instance.AddSkillpoints(1);
                _player.Damage(-1);
                _movementScript.CanMove = true;
                EnemySpawner.EnemyCount--;
            }
        }
    }

    private void StealCow()
    {
        if (!_movementScript.CanMove) 
            return;
        var hit = Physics2D.Raycast(transform.position, Vector2.down);
        var cow = hit.collider;
        if (cow == null) 
            return;
        if (cow.CompareTag("Cow"))
        {
            _currentCow = cow.gameObject.GetComponent<Cow>();
            _currentCow.IsMoving = true;
            _movementScript.CanMove = false;
        }
    }

}
