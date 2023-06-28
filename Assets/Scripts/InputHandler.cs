using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    public static event Action PressingShootButton;
    public static event Action HoldingShootButton;
    public static event Action ReleasingShootButton;

    private void Start()
    {      
        _playerInput.actions["ShootHolding"].canceled += OnReleasingShootButton;
    }

    private void Update()
    {
        if (_playerInput.actions["Shoot"].IsPressed())
        {
            OnPressingShootButton();
        }
        if (_playerInput.actions["ShootHolding"].IsInProgress())
        {
            OnHoldingShootButton();
        }
    }

    private void OnPressingShootButton()
    {
        PressingShootButton?.Invoke();
    }

    private void OnHoldingShootButton()
    {
        HoldingShootButton?.Invoke();
    }

    private void OnReleasingShootButton(InputAction.CallbackContext context)
    {
        context.ReadValueAsButton();
        ReleasingShootButton?.Invoke();
    }
}
