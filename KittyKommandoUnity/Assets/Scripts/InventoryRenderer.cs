using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Inventory))]
public class InventoryRenderer : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Image[] slotImages;
    private void OnValidate()
    {
        inventory = GetComponent<Inventory>();
    }

    public void Update()
    {
        var items = inventory.GetItems();
        for (int i = 0; i < items.Length; i++)
        {
            slotImages[i].sprite = items[i]?.GetSprite();
        }
    }
}