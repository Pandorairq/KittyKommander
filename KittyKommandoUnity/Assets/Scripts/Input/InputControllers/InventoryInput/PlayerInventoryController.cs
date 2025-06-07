using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input.InputControllers.InventoryInput
{
    public class PlayerInputController : InventoryController
    {
        private PlayerInputActions inputAction;
        
        public void Update()
        {
            var x = inputAction.Inventory.ScrollSlots.ReadValue<float>();
            onSlotChange.Invoke((int) x);
        }

        private void OnEnable()
        {
            inputAction = GetComponent<PlayerInputActionsWrapper>().GetPlayerInputActions();
            inputAction.Inventory.DropItem.performed += OnDropPressed;
        }

        private void OnDisable()
        {
            inputAction.Inventory.DropItem.performed -= OnDropPressed;
        }


        private void OnDropPressed(InputAction.CallbackContext context)
        {
            onDropItem.Invoke();
        }
    }
}
