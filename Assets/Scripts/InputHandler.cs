using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    public static event Action PressingShootButton;
    public static event Action HoldingShootButton;
    public static event Action ReleasingShootButton;

    public static event Action PauseMenuButtonPressed;
    public static event Action SkillMenuButtonPressed;

    private void Start()
    {
        _playerInput.actions["Shoot"].performed += OnPressingShootButton;
        _playerInput.actions["ShootHolding"].performed += OnHoldingShootButton;
        _playerInput.actions["ShootHolding"].canceled += OnReleasingShootButton;
        _playerInput.actions["OpenOrClosePauseMenu"].performed += OnPauseMenuButtonPressed;
        _playerInput.actions["OpenOrCloseSkillMenu"].performed += OnSkillMenuButtonPressed;
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

    private void OnReleasingShootButton(InputAction.CallbackContext context)
    {
        context.ReadValueAsButton();
        ReleasingShootButton?.Invoke();
    }

    private void OnPauseMenuButtonPressed(InputAction.CallbackContext context) 
    {
        context.ReadValueAsButton();
        PauseMenuButtonPressed?.Invoke();
    
    }

    private void OnSkillMenuButtonPressed(InputAction.CallbackContext context) 
    {
        context.ReadValueAsButton();
        SkillMenuButtonPressed?.Invoke();
    }
}
