using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Movement.States;
using Movement;
using FishNet.Object;

namespace Movement.States
{
    public class MovingPlatform : NetworkBehaviour
    {
        [Header("Platform Settings")]
        [SerializeField] private float speed;
        [SerializeField] protected Transform[] waypoints;

        private bool isQuitting = false;

        protected int targetPointIndex = 0;
        protected const int startingPointIndex = 0;
        private const float thresholdWaypointReached = 0.01f;

        protected virtual void Awake()
        {
            if (waypoints == null || waypoints.Length == 0)
            {
                Debug.LogError("MovingPlatform: No waypoints assigned at runtime.");
                return;
            }

            transform.position = waypoints[targetPointIndex].position;
        }

        protected virtual void Update()
        {
            if (!IsServerInitialized) return;

            MoveToNextWaypoint();
        }

        protected virtual void MoveToNextWaypoint()
        {
            if (HasReachedTarget())
            {
                AdvanceToNextPoint();
            }

            MoveTowardsWaypointIndex(targetPointIndex);
        }

        protected void MoveTowardsWaypointIndex(int index)
        {
            if (index < 0 || index >= waypoints.Length || waypoints[index] == null)
            {
                Debug.LogWarning("Invalid index on Moving Platform");
                return;
            }

            transform.position = Vector2.MoveTowards(transform.position, waypoints[index].position, speed * Time.deltaTime);
        }

        protected bool HasReachedTarget()
        {
            return Vector2.Distance(transform.position, waypoints[targetPointIndex].position) <= thresholdWaypointReached;
        }

        protected virtual void AdvanceToNextPoint()
        {
            targetPointIndex = (targetPointIndex + 1) % waypoints.Length;
        }

        private void SetPlayerParent(Transform player, Transform parent)
        {
            if (player != null)
            {
                player.SetParent(parent);
            }
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (!IsPlayer(collision.gameObject)) return;

            SetPlayerParent(collision.transform, transform);
        }

        protected virtual void OnCollisionExit2D(Collision2D collision)
        {
            if (isQuitting || !IsPlayer(collision.gameObject)) return;

            SetPlayerParent(collision.transform, null);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (!IsPlayer(collision.gameObject)) return;

            MovementComponent movementComponent = collision.gameObject.GetComponent<MovementComponent>();
            if (movementComponent == null) return;

            MovementState currentState = movementComponent.GetState();

            if (currentState is StandingState)
            {
                if (collision.transform.parent == null)
                {
                    SetPlayerParent(collision.transform, transform);
                }
            }
            else
            {
                SetPlayerParent(collision.transform, null);
            }
        }

        protected bool IsPlayer(GameObject obj)
        {
            return obj.layer == LayerMask.NameToLayer("Player");
        }

        private void OnApplicationQuit() => isQuitting = true;
        private void OnDestroy() => isQuitting = true;
    }
}