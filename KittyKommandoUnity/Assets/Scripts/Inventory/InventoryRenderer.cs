using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    [RequireComponent(typeof(Inventory))]
    public class InventoryRenderer : MonoBehaviour
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private Image[] slotImages;
        [SerializeField] private GameObject slotHighlight;
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

            var x = inventory.GetActiveSlot();
            slotHighlight.transform.position = slotImages[x].transform.position + Vector3.back;
        }
    }
}