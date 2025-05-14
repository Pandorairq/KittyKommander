using System;
using Movement.States;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement.InputControllers
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
        }

        private void OnDisable()
        {
            inputAction.Disable();
            inputAction.Player.Jump.performed -= OnJumpPressed;
            inputAction.Player.Jump.canceled -= OnJumpReleased;
        }

        private void OnJumpPressed(InputAction.CallbackContext context)
        {
            input.JumpInput = true;
        }

        private void OnJumpReleased(InputAction.CallbackContext context)
        {
            input.JumpInput = false;
        }
    }
}
