using System;
using System.Collections.Generic;
using Reset;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Movement
{
    
    [Serializable] 
    public enum CollisionHit
    {
        Top,
        Bottom,
        Right,
        Left,
    }

    public struct CollisionData
    {
        public bool IsSolid;
    }
    public class CollisionDetection : MonoBehaviour
    {
        [SerializeField] private CollisionHit hitDirection;
        [SerializeField] private MovementComponent movementComponent;
        private int numberOfCollidersTouching;
        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.GetComponent<IgnoreCollision>()) return;
            if(numberOfCollidersTouching++ != 0) return;
            
            var collisionData = new CollisionData
            {
                IsSolid = true
            };
            movementComponent.CollisionEnter(hitDirection, collisionData);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if(other.gameObject.GetComponent<IgnoreCollision>()) return;
            if(--numberOfCollidersTouching != 0) return;
            var collisionData = new CollisionData
            {
                IsSolid = true
            };
            movementComponent.CollisionExit(hitDirection, collisionData);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.GetComponent<IgnoreCollision>()) return;
            if(numberOfCollidersTouching++ != 0) return;
            var collisionData = new CollisionData
            {
                IsSolid = false
            };
            movementComponent.CollisionEnter(hitDirection, collisionData);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.gameObject.GetComponent<IgnoreCollision>()) return;
            if(--numberOfCollidersTouching != 0) return;
            var collisionData = new CollisionData
            {
                IsSolid = false
            };
            movementComponent.CollisionExit(hitDirection, collisionData);
        }
    }
}