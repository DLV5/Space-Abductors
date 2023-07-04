using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    void Start()
    {
        _playerInput.actions["OpenOrClosePauseMenu"].performed += OnPauseMenuButtonPressed;
        _playerInput.actions["OpenOrCloseSkillMenu"].performed += OnSkillMenuButtonPressed;
    }

    private void OnDestroy()
    {
        _playerInput.actions["OpenOrClosePauseMenu"].performed -= OnPauseMenuButtonPressed;
        _playerInput.actions["OpenOrCloseSkillMenu"].performed -= OnSkillMenuButtonPressed;   
    }

    private void OnPauseMenuButtonPressed(InputAction.CallbackContext context)
    {
        context.ReadValueAsButton();
        switch (GameManager.Instance.CurrentState)
        {
            case GameState.Playing:
                UIManager.Instance.OpenMenu(UIManager.Instance.PauseMenu);
                break;
            case GameState.Paused:
                UIManager.Instance.CloseMenu(UIManager.Instance.PauseMenu);
                break;
        }

    }

    private void OnSkillMenuButtonPressed(InputAction.CallbackContext context)
    {
        context.ReadValueAsButton();
        switch (GameManager.Instance.CurrentState)
        {
            case GameState.Playing:
                UIManager.Instance.OpenMenu(UIManager.Instance.SkillpointMenu);
                break;
            case GameState.Paused:              
                UIManager.Instance.CloseMenu(UIManager.Instance.SkillpointMenu);
                break;
        }
    }
}
