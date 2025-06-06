using Input.InputControllers;
using Input.InputControllers.Movement;
using UnityEngine;

namespace Movement.States
{
    
    public class FallingState : InAirState
    {
        public FallingState(Vector3 moveDirection)
        {
            MoveDirection = moveDirection;
        }
        public FallingState(Vector3 moveDirection, Vector3 currentExternalForce)
        {
            MoveDirection = moveDirection;
            ExternalForce = currentExternalForce;
        }
        public override MovementState HandleInput(MovementComponent movementComponent, MovementData movementData)
        {
            switch (movementData.HorizontalInput)
            {
                case > 0:
                    MoveDirection += Vector3.right;
                    //movementComponent.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case < 0:
                    MoveDirection += Vector3.left;
                    //movementComponent.transform.rotation = Quaternion.Euler(0, -180, 0);
                    break;
            }
            
            return null;
        }
        
        public override void OnStateEnter(MovementComponent movementComponent)
        {
            gravity = movementComponent.GetGravity();
        
        }

        public override void OnStateExit(MovementComponent movementComponent)
        {
        }

        public override MovementState OnCollisionEnter(CollisionData collisionData)
        {
            if (collisionData.Direction == CollisionDirection.Bottom) return new StandingState();
            if (!collisionData.IsSolid) return null;
            
            MoveDirection.x = 0;
            return null;
        }

        public override MovementState OnCollisionExit(CollisionData collisionData)
        {
            return null;
        }
        
        public override int GetStateID()
        {
            return 4;
        }
    }
}