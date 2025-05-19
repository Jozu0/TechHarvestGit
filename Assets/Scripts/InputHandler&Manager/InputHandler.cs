using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InputHandler : MonoBehaviour
{
    protected virtual void Start()
    {
        if (InputManager.Instance != null && InputManager.Instance.CurrentPlayerInput != null)
        {
            RegisterInputActions();
        }
        else
        {
            Debug.LogError($"InputHandler in {gameObject.name} can't be Init: InputManager not find or PlayerInput null");
        }
    }

    protected virtual void OnEnable()
    {
        if (InputManager.Instance != null && InputManager.Instance.CurrentPlayerInput != null)
        {
            RegisterInputActions();
        }
    }

    protected virtual void OnDisable()
    {
        if (InputManager.Instance != null && InputManager.Instance.CurrentPlayerInput != null)
        {
            UnregisterInputActions();
        }
    }

    protected abstract void RegisterInputActions();
    protected abstract void UnregisterInputActions();

    // Helper pour avoir facilement acc�s au PlayerInput
    protected PlayerInput GetPlayerInput()
    {
        if (InputManager.Instance != null)
        {
            return InputManager.Instance.CurrentPlayerInput;
        }
        return null;
    }
}
