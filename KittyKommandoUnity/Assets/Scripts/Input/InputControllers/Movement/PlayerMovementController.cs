using UnityEngine.InputSystem;

namespace Input.InputControllers.Movement
{
    public class PlayerMovementController : MovementController
    {
        private PlayerInputActions inputAction;
        private MovementData movement;
        
        public override MovementData GetInput()
        {
            return movement;
        }
        
        private void Awake()
        {
        }
        private void Update()
        {
            movement.HorizontalInput = inputAction.Movement.HorizontalMovement.ReadValue<float>();
        }

        private void OnEnable()
        {
            inputAction = GetComponent<PlayerInputActionsWrapper>().GetPlayerInputActions();
            inputAction.Enable();
            inputAction.Movement.Jump.performed += OnJumpPressed;
            inputAction.Movement.Jump.canceled += OnJumpReleased;
            inputAction.Movement.Running.performed += OnRunningPressed;
            inputAction.Movement.Running.canceled += OnRunningReleased;
        }

        private void OnDisable()
        {
            inputAction.Disable();
            inputAction.Movement.Jump.performed -= OnJumpPressed;
            inputAction.Movement.Jump.canceled -= OnJumpReleased;
            inputAction.Movement.Running.performed -= OnRunningPressed;
            inputAction.Movement.Running.canceled -= OnRunningReleased;
        }

        private void OnJumpPressed(InputAction.CallbackContext context)
        {
            movement.Jump = true;
        }

        private void OnJumpReleased(InputAction.CallbackContext context)
        {
            movement.Jump = false;
        }

        private void OnRunningPressed(InputAction.CallbackContext context)
        {
            movement.Running = true;
        }

        private void OnRunningReleased(InputAction.CallbackContext context)
        {
            movement.Running = false;
        }
    }
}
