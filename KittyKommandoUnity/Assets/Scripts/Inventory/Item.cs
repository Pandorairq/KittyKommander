using Action;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private Sprite inventorySprite;
        
        public UnityEvent<Inventory> onPickUp;
        public UnityEvent onHold;
        public UnityEvent onStore;
        public UnityEvent onDrop;
        private bool isHeld;
        
        private SpriteRenderer spriteRenderer;
        private new Collider2D collider2D;
        private Transform originalParent;
        public void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            collider2D = GetComponent<Collider2D>();
            originalParent = transform.parent;
        }

        /// <summary>
        /// Called when the item is picked up from the game world
        /// </summary>
        public void PickUp(Interactor interactor)
        {
            var inventory = interactor.GetComponent<Inventory>();
            if (!inventory)
            {
                inventory = GetComponentInChildren<Inventory>();
                print(inventory);
            }
            inventory?.AddItem(this);
            transform.SetParent( inventory ? inventory.transform : originalParent);
            onPickUp.Invoke(inventory);
        }
    
        /// <summary>
        /// Activates the item
        /// </summary>
        public void Hold()
        {
            if (spriteRenderer)
            {
                spriteRenderer.enabled = true;
            }

            if (collider2D)
            {
                collider2D.enabled = true;
            }
            onHold.Invoke();
        }
    
        /// <summary>
        /// Disables the item
        /// </summary>
        public void Store()
        {
            if (spriteRenderer)
            {
                spriteRenderer.enabled = false;
            }

            if (collider2D)
            {
                collider2D.enabled = false;
            }
            onStore.Invoke();
        }
    
        /// <summary>
        /// Called when the item is added back into the game world
        /// </summary>
        public void Drop()
        {
            transform.SetParent(originalParent);
            onDrop.Invoke();
        }

        public Sprite GetSprite()
        {
            return inventorySprite;
        }
    }
}