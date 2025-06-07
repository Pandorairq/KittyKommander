using UnityEngine;
using UnityEngine.Events;

namespace Input.InputControllers.InventoryInput
{
    public abstract class InventoryController: MonoBehaviour
    {
        public UnityEvent onDropItem;
        public UnityEvent<int> onSlotChange;
    }
}
