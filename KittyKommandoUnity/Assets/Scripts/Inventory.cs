using System;
using Input.InputControllers.Action;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] private ActionController actionController;
    
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
}