using System;
using Input.InputControllers.InventoryInput;
using UnityEngine;

namespace Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private int inventorySize;
        [SerializeField] private InventoryController inventoryController;
        
        public Item[] items;
        private int activeSlot;
        
        private void Awake()
        {
            items = new Item[inventorySize];
            activeSlot = 0;
        }

        private void Update()
        {
            var slotChange = inventoryController.GetSlotChange();
            IncrementActiveSlot(slotChange);
        }
        public void AddItem(Item item)
        {
            DropCurrentItem();
            items[activeSlot] = item;
            item.Hold();
        }

        public void DropCurrentItem()
        {
            items[activeSlot]?.Drop();
            items[activeSlot] = null;
        }

        public void IncrementActiveSlot(int amount)
        {
            items[activeSlot]?.Store();
            activeSlot = (activeSlot + amount) % inventorySize;
            if (activeSlot < 0)
            {
                activeSlot += inventorySize;
            }
            items[activeSlot]?.Hold();
        }
        
        public Item[] GetItems()
        {
            var itemsClone = new Item[inventorySize];
            Array.Copy(items, itemsClone, inventorySize);
            return itemsClone;
        }
        
        public int GetInventorySize()
        {
            return inventorySize;
        }

        public int GetActiveSlot()
        {
            return activeSlot;
        }

        public InventoryController GetInventoryController()
        {
            return inventoryController;
        }
    }
}