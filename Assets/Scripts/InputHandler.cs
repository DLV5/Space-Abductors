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
        _playerInput.actions["Shoot"].performed += OnPressingShootButton;
        _playerInput.actions["ShootHolding"].performed += OnHoldingShootButton;
        _playerInput.actions["ShootHolding"].canceled += OnReleasingShootButton;
    }

    private void OnPressingShootButton(InputAction.CallbackContext context)
    {
        context.ReadValueAsButton();
        PressingShootButton?.Invoke();
    }

    private void OnHoldingShootButton(InputAction.CallbackContext context)
    {
        context.ReadValueAsButton();
        HoldingShootButton?.Invoke();
    }

    private void OnReleasingShootButton( InputAction.CallbackContext context)
    {
        context.ReadValueAsButton();
        ReleasingShootButton?.Invoke();
    }
}
