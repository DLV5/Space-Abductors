using UnityEngine;

public class RailgunWeapon : Weapon
{
    [Tooltip("Damage multiplying by this value every step of charge (there 3 steps)")]
    [SerializeField] private int _damageMultiplier = 1;

    public int DamageMultiplier 
    {
        get => _damageMultiplier;
        private set => _damageMultiplier = value;
    }

    private GameObject _rayObject;
    private RailgunRay _ray;
    private Animator _rayAnimator;

    private void Start()
    {
        _rayObject = transform.GetChild(0).gameObject;
        _ray = _rayObject.GetComponent<RailgunRay>();
        _rayAnimator = _rayObject.GetComponent<Animator>();

        //note, that damage of a railgun depends on how long it has been holded
        _ray.Damage = _damage;
        InputHandler.PressingShootButton += InputHandler_OnPressingShootButton;
        InputHandler.ReleasingShootButton += InputHandler_OnReleasingShootButton;
    }

    private void OnDestroy()
    {
        InputHandler.PressingShootButton -= InputHandler_OnPressingShootButton;
        InputHandler.ReleasingShootButton -= InputHandler_OnReleasingShootButton;
    }


    private void InputHandler_OnPressingShootButton()
    {
        EnableRailgun();
    }

    private void InputHandler_OnReleasingShootButton()
    {
        Fire();
        DisableRailgun();
    }

    protected override void Fire()
    {
        _ray.CastRayThroughMousePosition();
    }

    private void EnableRailgun()
    {
        _rayObject.SetActive(true);
        _rayAnimator.SetTrigger("IsCharged");
    }
    private void DisableRailgun()
    {
        _rayAnimator.SetTrigger("IsReleased");
    }
}
