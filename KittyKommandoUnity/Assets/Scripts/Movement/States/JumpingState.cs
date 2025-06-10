using Input.InputControllers;
using Input.InputControllers.Movement;
using UnityEngine;

namespace Movement.States
{
    public class JumpingState : InAirState
    {
        public JumpingState(Vector3 moveDirection)
        {
            MoveDirection = moveDirection;
        }
        public JumpingState(Vector3 moveDirection, Vector3 currentExternalForce)
        {
            MoveDirection = moveDirection;
            ExternalForce = currentExternalForce;
        }
        public override MovementState HandleInput(MovementComponent movementComponent, MovementData movementData)
        {
            
            switch (movementData.HorizontalInput)
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
            return MoveDirection.y <= 0 ? new FallingState(MoveDirection, ExternalForce) : null;
        }

        public override void OnStateEnter(MovementComponent movementComponent)
        {
            gravity = movementComponent.GetGravity();
            movementComponent.Move(Vector3.up * movementComponent.GetJumpHeight());
            //MoveDirection += Vector3.up * movementComponent.GetJumpHeight();
        }

        public override void OnStateExit(MovementComponent movementComponent)
        {
        }
        public override MovementState OnCollisionEnter(CollisionData collisionData)
        {
            return collisionData is { Direction: CollisionDirection.Top, IsSolid: true } ? new FallingState(Vector3.zero) : null;
        }

        public override MovementState OnCollisionExit(CollisionData collisionData)
        {
            return null;
        }
        public override int GetStateID()
        {
            return 3;
        }
    }
}