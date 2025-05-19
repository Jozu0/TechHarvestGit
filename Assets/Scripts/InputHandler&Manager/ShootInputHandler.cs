using UnityEngine;
using UnityEngine.InputSystem;

public class ShootInputHandler : InputHandler
{
    [SerializeField] private PlayerShoot playerShoot;
    
    protected override void RegisterInputActions()
    {
        PlayerInput playerInput = GetPlayerInput();
        if (playerInput != null)
        {
            playerInput.actions["Jump"].started += OnShootStarted;

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
            playerInput.actions["Jump"].started -= OnShootStarted;
        }
    }

    private void OnShootStarted(InputAction.CallbackContext context)
    {
        if (playerShoot != null)
        {
            playerShoot.SpawnBullet();
        }
        else
        {
            Debug.LogError("PlayerController non assign� dans MovementInputHandler");
        }
    }

}