using UnityEngine;
using UnityEngine.InputSystem;


public class MouseInputHandler : InputHandler
{
// Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private ObjectOnGrid objectOnGrid;

    protected override void RegisterInputActions()
    {
        PlayerInput playerInput = GetPlayerInput();
        if (playerInput != null)
        {
            playerInput.actions["Cancel"].started += OnMoveStarted;
        }
        else
        {
            Debug.LogError("PlayerInput is null in MovementInputHandler");
        }
    }

    protected override void UnregisterInputActions()
    {
        PlayerInput playerInput = GetPlayerInput();
        if (playerInput != null)
        {
            playerInput.actions["Cancel"].started -= OnMoveStarted;

        }
    }

    private void OnMoveStarted(InputAction.CallbackContext context)
    {
        if (objectOnGrid != null)
        {
            objectOnGrid.OnMouseCancel();
        }
        else
        {
            Debug.LogError("PlayerController non assign� dans MovementInputHandler");
        }
    }

}
