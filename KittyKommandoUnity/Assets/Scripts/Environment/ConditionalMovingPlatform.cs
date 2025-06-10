using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    public class ConditionalMovingPlatform : MovingPlatform
    {
        [Header("Activation Settings")]
        [SerializeField, Min(1)] private int nrPlayersNeededActivation = 1;
        private HashSet<GameObject> playersOnPlatformSet = new();
        private const float activationDelay = 0.3f; 
        private float activationTimer = 0f;
        private bool waitingToActivate = false;

        protected override void Update()
        {
            if (!IsServerInitialized) return;

            if (!waitingToActivate)
            {
                if (playersOnPlatformSet.Count >= nrPlayersNeededActivation)
                {
                    MoveToNextWaypoint();
                }
                else
                {
                    ReturnToStart();
                }
            }
            else
            {
                activationTimer += Time.deltaTime;
                if (activationTimer >= activationDelay)
                {
                    waitingToActivate = false;
                }
            }
        }

        protected override void AdvanceToNextPoint()
        {
            if (targetPointIndex < (waypoints.Length - 1))
            {
                targetPointIndex++;
            }
        }

        private void ReturnToStart()
        {
            targetPointIndex = startingPointIndex;
            MoveTowardsWaypointIndex(startingPointIndex);
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if (!IsPlayer(collision.gameObject)) return;
            base.OnCollisionEnter2D(collision);

            if (!playersOnPlatformSet.Contains(collision.gameObject))
            {
                playersOnPlatformSet.Add(collision.gameObject);
                waitingToActivate = true;
                activationTimer = 0f;
            }
        }

        protected override void OnCollisionExit2D(Collision2D collision)
        {
            if (!IsPlayer(collision.gameObject)) return;
            base.OnCollisionExit2D(collision);

            playersOnPlatformSet.Remove(collision.gameObject);
            waitingToActivate = false;
        }

        private void OnDestroy()
        {
            playersOnPlatformSet.Clear();
        }
    }
}
