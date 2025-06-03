using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInputHandler : InputHandler
{
    [SerializeField] private CameraMove cameraMove;
    private Vector2 moveInput;

    protected override void RegisterInputActions()
    {
        PlayerInput playerInput = GetPlayerInput();
        if (playerInput != null)
        {
            playerInput.actions["Move"].performed += OnMovePerformed;
            playerInput.actions["Move"].canceled += OnMoveCanceled;
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
            playerInput.actions["Move"].performed -= OnMovePerformed;
            playerInput.actions["Move"].canceled -= OnMoveCanceled;
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (cameraMove != null)
        {
            cameraMove.SetMoveDirection(moveInput);
        }
        else
        {
            Debug.LogError("PlayerController non assign� dans MovementInputHandler");
        }
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
        if (cameraMove != null)
        {
            cameraMove.SetMoveDirection(moveInput);
        }
    }
}
