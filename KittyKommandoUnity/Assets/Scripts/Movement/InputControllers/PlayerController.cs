using System;
using Movement.States;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement.InputControllers
{
    public class PlayerController : InputController
    {
        private SpriteFlipper flipper;
        private PlayerInputActions inputAction;
        private InputData input;
        
        public override InputData GetInput()
        {
            return input;
        }

        private void Awake()
        {
            inputAction = new PlayerInputActions();
            flipper = GetComponentInChildren<SpriteFlipper>();
        }
        private void Update()
        {
            input.HorizontalInput = inputAction.Player.HorizontalMovement.ReadValue<float>();

            if (flipper != null)
            {
                Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
                flipper.Flip(direction);
            }
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
            input.Jump = true;
        }

        private void OnJumpReleased(InputAction.CallbackContext context)
        {
            input.Jump = false;
        }
    }
}
