using UnityEngine;

public class FlamethrowerWeapon : Weapon
{
    public static FlamethrowerWeapon Instance;

    public int DamageTicksPerSecond = 3;

    [SerializeField] ParticleSystem _flames;
    private Collider2D _flameCollider;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected void Start()
    {
        _flameCollider = GetComponent<Collider2D>();
        _flameCollider.enabled = false;
        InputHandler.PressingShootButton += InputHandler_OnPressingShootButton;
        InputHandler.ReleasingShootButton += InputHandler_OnReleasingShootButton;
    }

    private void OnDestroy()
    {
        InputHandler.PressingShootButton -= InputHandler_OnPressingShootButton;
        InputHandler.ReleasingShootButton -= InputHandler_OnReleasingShootButton;
    }

    protected virtual void InputHandler_OnPressingShootButton()
    {
        Fire();
    }
    
    protected virtual void InputHandler_OnReleasingShootButton()
    {
        StopFire();
    }

    private void StopFire()
    {
        _flames.Stop();
        _flameCollider.enabled = false;
    }

    protected override void Shoot()
    {
        _flames.Play();
        _flameCollider.enabled = true;
    }
}
