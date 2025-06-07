using System;
using Input.InputControllers.Action;
using Input.InputControllers.InventoryInput;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    public InventoryItem[] items;
    private int activeSlot;

    private void Awake()
    {
        items = new InventoryItem[inventorySize];
        activeSlot = 0;
    }
    public void AddItem(InventoryItem item)
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
        while (activeSlot < 0)
        {
            activeSlot += inventorySize;
        }
        print(activeSlot);
        items[activeSlot]?.Hold();
    }

    public InventoryItem[] GetItems()
    {
        var itemsClone = new InventoryItem[inventorySize];
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
}