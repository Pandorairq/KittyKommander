using System;
using Action;
using Input.InputControllers.InventoryInput;
using Unity.Mathematics;
using UnityEngine;

namespace Inventory.Items
{
    public class FollowMouse : MonoBehaviour
    {
        [SerializeField] private Item item;
        [SerializeField] private float distance;
        [SerializeField] private float speed;
        [SerializeField] private Rigidbody2D rigidBody2D;
        [SerializeField] private DistanceJoint2D joint;
        
        private InventoryController inventoryController;
        private float currentAngle;

        public void Start()
        {
            joint.enabled = false;
        }

        public void FixedUpdate()
        {
            if(!inventoryController) return;
            var itemDirection = inventoryController.GetActiveItemPosition();
            //rigidBody2D.AddForce(itemDirection * speed * Time.fixedDeltaTime);
            rigidBody2D.linearVelocity = itemDirection * speed * Time.fixedDeltaTime;

            var currentDirection = transform.position - transform.parent.position;
            var singedAngle = Vector2.SignedAngle(Vector2.up, currentDirection);
            var newRotation = transform.localRotation.eulerAngles;
            newRotation.z = singedAngle;
            transform.localRotation = Quaternion.Euler(newRotation);
        }

        public void SetPositionAndRotation(float rot)
        {
            float angleRadians = rot * Mathf.Deg2Rad;
    
            float x = -Mathf.Sin(angleRadians); 
            float y = Mathf.Cos(angleRadians);
    
            var newPos = new Vector2(x, y).normalized * distance; 
            var newRotation = transform.localRotation.eulerAngles;
            newRotation.z = rot;
            transform.localRotation = Quaternion.Euler(newRotation);
            
        }
        public void Awake()
        {
            item.onPickUp.AddListener(OnPickUp);
            item.onDrop.AddListener(OnDrop);
        }
        public void OnDestroy()
        {
            item.onPickUp.RemoveListener(OnPickUp);
            item.onDrop.RemoveListener(OnDrop);
        }

        private void OnPickUp(Inventory inventory)
        {
            inventoryController = inventory.GetInventoryController();
            joint.connectedBody = inventory.GetComponentInParent<Rigidbody2D>();
            joint.enabled = true;
        }

        private void OnDrop()
        {
            inventoryController = null;
            joint.connectedBody = null;
            joint.enabled = false;
        }
    }
}