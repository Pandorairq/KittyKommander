using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Movement
{ 
    [Serializable] 
    public enum CollisionDirection
    {
        Top,
        Bottom,
        Right,
        Left,
    }

    public struct CollisionData
    {
        public bool IsHit;
        public CollisionDirection Direction;
        public bool IsSolid;
    }
    
    public class CollisionDetection : MonoBehaviour
    {
        public UnityEvent<CollisionData> CollisionEnter = new ();
        public UnityEvent<CollisionData> CollisionExit = new ();
        
        [SerializeField] private LayerMask collisionLayer;
        [SerializeField] private float castDistance = 0.1f;
        [SerializeField] private Vector2 size;
        [SerializeField] private float shrinkMargin;
        [SerializeField] private new Collider2D collider;

        private CollisionData[] collisions;
        public void Start()
        {

            collisions = new CollisionData[4];
        }

        public void CheckCollisions()
        {
            Vector2 castOrigin = (Vector2)transform.position + collider.offset;
            
            Vector2 offset = new Vector2(0, size.y / 4);
            Vector2 castSize = new Vector2(size.x - shrinkMargin, size.y / 2); 
            RaycastHit2D hitUp = Physics2D.BoxCast(castOrigin + offset, castSize, 0f, Vector2.up, castDistance, collisionLayer);
            RaycastHit2D hitDown = Physics2D.BoxCast(castOrigin - offset, castSize, 0f, Vector2.down, castDistance, collisionLayer);
            
            /*
            offset = new Vector2(size.x / 4, 0);
            castSize = new Vector2(size.x / 2, size.y - shrinkMargin); 
            RaycastHit2D hitRight = Physics2D.BoxCast(castOrigin + offset, castSize, 0f, Vector2.right, castDistance, collisionLayer);
            RaycastHit2D hitLeft = Physics2D.BoxCast(castOrigin - offset, castSize, 0f, Vector2.left, castDistance, collisionLayer);
            SetCollisionData(hitRight, CollisionDirection.Right);
            SetCollisionData(hitLeft, CollisionDirection.Left);
            */ 
            
            SetCollisionData(hitUp, CollisionDirection.Top);
            SetCollisionData(hitDown, CollisionDirection.Bottom);
        }

        private void SetCollisionData(RaycastHit2D hit, CollisionDirection direction)
        {
            int index = (int)direction;
            if (hit)
            {
                var wasHitBefore = collisions[index].IsHit;
                collisions[index].IsHit = true;
                collisions[index].Direction = direction;
                if (!wasHitBefore)
                {
                    CollisionEnter.Invoke(collisions[index]);
                }
            }
            else
            {
                var wasHitBefore = collisions[index].IsHit;
                collisions[index].IsHit = false;
                if (wasHitBefore)
                {
                    CollisionExit.Invoke(collisions[index]);
                }
            }
        }
    }
}