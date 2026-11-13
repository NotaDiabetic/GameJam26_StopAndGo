using UnityEngine;
using UnityEngine.InputSystem;

public class InputControls : MonoBehaviour
{
    private InputSystem_Actions _inputActions;
    private InputAction _move;
    private InputAction _jump;

    [SerializeField] private PlayerControls player;

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
        player.horizontal = ctx.ReadValue<Vector2>().x;
    }
    private void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && player.IsGrounded() && player.movementBlocked == false)
        {
            player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x, player.jumpingPower);
        }
        if (ctx.canceled && player.rb.linearVelocity.y > 0f)
        {
            player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x, player.rb.linearVelocity.y * 0.5f);
        }
    }
}
