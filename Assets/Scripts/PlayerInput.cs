using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event EventHandler OnInteractAction; // E tuþu event'i

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        // E'ye basýlýnca event'i tetikle
        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy()
    {
        // Memory leak'i önlemek için unsubscribe et
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Dispose();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector= inputVector.normalized;

        return inputVector;
    }

    
}
