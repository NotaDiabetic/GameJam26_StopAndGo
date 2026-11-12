using UnityEngine;
using UnityEngine.InputSystem;

public class InputControls : MonoBehaviour
{
    private InputSystem_Actions _inputActions;
    private InputAction _move;
    private InputAction _jump;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _move = _inputActions.BEAN.movement;
        _jump = _inputActions.BEAN.Jump;
    }
    public void OnEnable()
    {
        _inputActions.Enable();
        SubscribeInputs();
    }
    public void OnDisable()
    {
        _inputActions.Disable();
        UnsubscribeInputs();
    }
    private void SubscribeInputs()
    {
        _move.performed += Move;
        _move.canceled += Move;

        _jump.performed += Jump;
        _jump.canceled += Jump;

    }
    private void UnsubscribeInputs()
    {
        _move.performed -= Move;
        _move.canceled -= Move;

        _jump.performed -= Jump;
        _jump.canceled -= Jump;
    }
    private void Move(InputAction.CallbackContext ctx)
    {
        
    }
    private void Jump(InputAction.CallbackContext ctx)
    {

    }
}
