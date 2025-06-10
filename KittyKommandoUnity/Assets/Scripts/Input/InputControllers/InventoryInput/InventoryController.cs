using UnityEngine;
using UnityEngine.Events;

namespace Input.InputControllers.InventoryInput
{
    public abstract class InventoryController: MonoBehaviour
    {
        public UnityEvent onDropItem;
        protected int slotChange;
        protected Vector2 activeItemPosition;
        public int GetSlotChange()
        {
            return slotChange;
        }

        public Vector2 GetActiveItemPosition()
        {
            return activeItemPosition;
        }
    }
}
