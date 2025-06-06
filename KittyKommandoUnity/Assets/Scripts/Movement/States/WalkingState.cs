using Input.InputControllers;
using Input.InputControllers.Movement;
using UnityEngine;

namespace Movement.States
{
    
    public class WalkingState : GroundedState
    {
        public WalkingState(Vector3 direction)
        {
            MoveDirection = direction;
        }
        public override MovementState HandleInput(MovementComponent movementComponent, MovementData movementData)
        {
            if (movementData.Jump)
            {
                switch (movementData.HorizontalInput)
                {
                    case >0:
                        return new JumpingState(MoveDirection + Vector3.right, ExternalForce);
                    case <0:
                        return new JumpingState(MoveDirection + Vector3.left, ExternalForce);
                    case 0:
                        return new JumpingState(MoveDirection, ExternalForce);
                }
            }
            switch (movementData.HorizontalInput)
            {
                case >0:
                    MoveDirection += Vector3.right;
                    if (movementData.Running) return new RunningState(MoveDirection);
                    break;
                case <0:
                    MoveDirection += Vector3.left;
                    if (movementData.Running) return new RunningState(MoveDirection);
                    break;
                case 0:
                    return new StandingState();
            }
            return null;
        }

        public override void OnStateEnter(MovementComponent movementComponent)
        {
        }


        public override void OnStateExit(MovementComponent movementComponent) { }

        public override MovementState OnCollisionEnter(CollisionData collisionData)
        {
            return null;
        }

        public override MovementState OnCollisionExit(CollisionData collisionData)
        { 
            if(collisionData.Direction == CollisionDirection.Bottom) return new FallingState(Vector3.zero);
            return null;
        }

        public override int GetStateID()
        {
            return 1;
        }
    }
}