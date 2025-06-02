using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Movement.States;
using Movement;

namespace Movement.States
{
    public class MovingPlatform : MonoBehaviour
    {
        public float speed;
        public Transform[] waypoints;
        [SerializeField] private MovementComponent movementComponent;
        private bool isQuitting = false;

        private int targetPointIndex = 0;

        void Awake()
        {
            if (waypoints == null || waypoints.Length == 0)
            {
                Debug.LogError("MovingPlatform: No waypoints assigned!");
                enabled = false;
                return;
            }

            if (movementComponent == null)
            {
                Debug.LogWarning("MovingPlatform: movementComponent is not assigned.");
                enabled = false;
                return;
            }
            transform.position = waypoints[targetPointIndex].position;
        }

        void Update()
        {
            if (Mathf.Approximately(Vector2.Distance(transform.position, waypoints[targetPointIndex].position), 0f))
            {
                AdvanceToNextPoint();
            }

            transform.position = Vector2.MoveTowards(transform.position, waypoints[targetPointIndex].position, speed * Time.deltaTime);
        }

        private void AdvanceToNextPoint()
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

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!IsPlayer(other.gameObject)) return;
            SetPlayerParent(other.transform, transform);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (isQuitting) return;
            if (!IsPlayer(other.gameObject)) return;
            SetPlayerParent(other.transform, null);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (!IsPlayer(other.gameObject)) return;
            MovementState currentState = movementComponent.GetState();

            if (currentState is StandingState)
            {
                if (other.transform.parent == null)
                {
                    SetPlayerParent(other.transform, transform);
                }
            }
            else
            {
                SetPlayerParent(other.transform, null);
            }
        }

        private bool IsPlayer(GameObject obj)
        {
            return obj.CompareTag("Player");
        }

        private void OnApplicationQuit()
        {
            isQuitting = true;
        }

        private void OnDestroy()
        {
            isQuitting = true;
        }
    }
}