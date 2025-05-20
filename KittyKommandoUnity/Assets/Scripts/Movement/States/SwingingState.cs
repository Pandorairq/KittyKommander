using UnityEngine;

namespace Movement.States
{
    public class SwingingState : MovementState
    {
        public override MovementState HandleInput(MovementComponent movementComponent, InputData inputData)
        {
            if (!inputData.JumpInput) return null;
            return inputData.HorizontalInput switch
            {
                > 0 => new JumpingState(MoveDirection + Vector3.right, ExternalForce),
                < 0 => new JumpingState(MoveDirection + Vector3.left, ExternalForce),
                0 => new JumpingState(MoveDirection, ExternalForce),
                _ => null
            };
        }

        public override void Update(MovementComponent movementComponent, float deltaTime)
        {
            viewDirection = MoveDirection.x;
            movementComponent.Move(MoveDirection + ExternalForce);
            MoveDirection = Vector3.zero;
            ExternalForce = Vector3.zero;
        }

        public override void OnStateEnter(MovementComponent movementComponent)
        {
           
        }

        public override void OnStateExit(MovementComponent movementComponent)
        {
            
        }

        public override MovementState OnCollisionEnter(CollisionData collisionData)
        {
            return null;
        }

        public override MovementState OnCollisionExit(CollisionData collisionData)
        {
            return null;
        }

        public override int GetStateID()
        {
            return 6;
        }
    }
}