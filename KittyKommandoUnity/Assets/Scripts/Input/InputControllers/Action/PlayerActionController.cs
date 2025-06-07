using UnityEngine;
using UnityEngine.InputSystem;

namespace Input.InputControllers.Action
{
    public class PlayerActionController : ActionController
    {
        private PlayerInputActions inputAction;
        
        private void Awake()
        {
        }

        private void OnEnable()
        {
            inputAction = GetComponent<PlayerInputActionsWrapper>().GetPlayerInputActions();
            inputAction.Action.Interact.performed += OnActionPressed;
            inputAction.Action.Interact.canceled += OnActionReleased;
        }

        private void OnDisable()
        {
            inputAction.Action.Interact.performed -= OnActionPressed;
            inputAction.Action.Interact.canceled -= OnActionReleased;
        }

        private void OnActionPressed(InputAction.CallbackContext context)
        {
            onActionStarted.Invoke();
        }

        private void OnActionReleased(InputAction.CallbackContext context)
        {
            onActionStopped.Invoke();
        }

        private void OnDropPressed(InputAction.CallbackContext context)
        {
            onDrop.Invoke();
        }
    }
}
