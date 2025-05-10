using Reset;
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
        public override MovementState HandleInput(MovementComponent movementComponent, InputData inputData)
        {
            if (InAirMovementEnabled)
            {
                switch (inputData.HorizontalInput)
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
            }
            return null;
        }
        
        public override void OnStateEnter(MovementComponent movementComponent)
        {
            Gravity = movementComponent.GetGravity();
        
        }

        public override void OnStateExit(MovementComponent movementComponent)
        {
        }

        public override MovementState OnCollisionEnter(CollisionHit collisionDirection, CollisionData collisionData)
        {
            if (collisionDirection == CollisionHit.Bottom) return new StandingState();
            if (!collisionData.IsSolid) return null;
            
            MoveDirection.x = 0;
            return null;
        }

        public override MovementState OnCollisionExit(CollisionHit collisionDirection, CollisionData collisionData)
        {
            return null;
        }
        
        public override int GetStateID()
        {
            return 4;
        }
    }
}