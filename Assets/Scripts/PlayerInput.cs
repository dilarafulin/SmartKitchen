using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector= inputVector.normalized;

        return inputVector;
    }

    public bool GetInteractPressed()
    {
        return playerInputActions.Player.Interact.WasPressedThisFrame();
        //WasPressedThisFrame: current frame'de tusa basildiysa true, yoksa false 
    }
}
