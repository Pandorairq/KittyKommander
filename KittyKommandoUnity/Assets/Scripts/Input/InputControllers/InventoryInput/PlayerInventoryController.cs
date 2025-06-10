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
            slotChange = (int) inputAction.Inventory.ScrollSlots.ReadValue<float>();
            
            var itemPosition = inputAction.Inventory.ItemPosition.ReadValue<Vector2>();
            if (itemPosition.magnitude > 1) // mouseInput needs to be transformed but its buggy in bottom left corner
            {
                var screenPosOfPlayer= Camera.main.WorldToScreenPoint(transform.position);
                itemPosition -= (Vector2) screenPosOfPlayer;
                itemPosition = itemPosition.normalized;
            }

            activeItemPosition = itemPosition;
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
