using Reset;
using UnityEngine;

namespace Movement.States
{
    public class JumpingState : InAirState
    {
        public JumpingState(Vector3 moveMoveDirection)
        {
            MoveDirection = moveMoveDirection;
        }
        public JumpingState(Vector3 moveMoveDirection, Vector3 currentExternalForce)
        {
            MoveDirection = moveMoveDirection;
            ExternalForce = currentExternalForce;
        }
        public override MovementState HandleInput(MovementComponent movementComponent, InputData inputData)
        {
            if (InAirMovementEnabled)
            {
                switch (inputData.HorizontalInput)
                {
                    case >0:
                        MoveDirection += Vector3.right;
                        //movementComponent.transform.rotation = Quaternion.Euler(0,0, 0);
                        break;
                    case <0:
                        MoveDirection += Vector3.left;
                        //movementComponent.transform.rotation = Quaternion.Euler(0,-180, 0);
                        break;
                }
            }
            return MoveDirection.y <= 0 ? new FallingState(MoveDirection, ExternalForce) : null;
        }

        public override void OnStateEnter(MovementComponent movementComponent)
        {
            Gravity = movementComponent.GetGravity();
            MoveDirection += Vector3.up * movementComponent.GetJumpHeight();

        }

        public override void OnStateExit(MovementComponent movementComponent)
        {
        }
        public override MovementState OnCollisionEnter(CollisionHit collisionDirection, CollisionData collisionData)
        {
            return collisionDirection == CollisionHit.Top && collisionData.IsSolid ? new FallingState(Vector3.zero) : null;
        }

        public override MovementState OnCollisionExit(CollisionHit collisionDirection, CollisionData collisionData)
        {
            return null;
        }
        public override int GetStateID()
        {
            return 3;
        }
    }
}