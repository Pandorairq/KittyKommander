using UnityEngine.InputSystem;

namespace Input.InputControllers
{
    public class PlayerController : InputController
    {
        private PlayerInputActions inputAction;
        private InputData input;
        
        public override InputData GetInput()
        {
            return input;
        }
        
        private void Awake()
        {
            inputAction = new PlayerInputActions();
        }
        private void Update()
        {
            input.HorizontalInput = inputAction.Player.HorizontalMovement.ReadValue<float>();
        }

        private void OnEnable()
        {
            inputAction.Enable();
            inputAction.Player.Jump.performed += OnJumpPressed;
            inputAction.Player.Jump.canceled += OnJumpReleased;
            inputAction.Player.Running.performed += OnRunningPressed;
            inputAction.Player.Running.canceled += OnRunningReleased;
            inputAction.Player.Action.performed += OnActionPressed;
            inputAction.Player.Action.canceled += OnActionReleased;
        }

        private void OnDisable()
        {
            inputAction.Disable();
            inputAction.Player.Jump.performed -= OnJumpPressed;
            inputAction.Player.Jump.canceled -= OnJumpReleased;
            inputAction.Player.Running.performed -= OnRunningPressed;
            inputAction.Player.Running.canceled -= OnRunningReleased;
            inputAction.Player.Action.performed -= OnActionPressed;
            inputAction.Player.Action.canceled -= OnActionReleased;
        }

        private void OnJumpPressed(InputAction.CallbackContext context)
        {
            onJumpButton.Invoke(true);
            input.Jump = true;
        }

        private void OnJumpReleased(InputAction.CallbackContext context)
        {
            onJumpButton.Invoke(false);
            input.Jump = false;
        }

        private void OnRunningPressed(InputAction.CallbackContext context)
        {
            onRunningButton.Invoke(true);
            input.Running = true;
        }

        private void OnRunningReleased(InputAction.CallbackContext context)
        {
            onRunningButton.Invoke(false);
            input.Running = false;
        }
        private void OnActionPressed(InputAction.CallbackContext context)
        {
            onActionButton.Invoke(true);
            input.Action = true;
        }

        private void OnActionReleased(InputAction.CallbackContext context)
        {
            onActionButton.Invoke(false);
            input.Action = false;
        }
    }
}
