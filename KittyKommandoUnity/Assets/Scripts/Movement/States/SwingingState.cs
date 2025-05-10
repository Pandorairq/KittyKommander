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

        public override float Update(MovementComponent movementComponent, float deltaTime)
        {
            var viewDirection = MoveDirection.x;
            movementComponent.Move(MoveDirection + ExternalForce);
            MoveDirection = Vector3.zero;
            ExternalForce = Vector3.zero;
            return viewDirection;
        }

        public override void OnStateEnter(MovementComponent movementComponent)
        {
           
        }

        public override void OnStateExit(MovementComponent movementComponent)
        {
            
        }

        public override MovementState OnCollisionEnter(CollisionHit collisionDirection, CollisionData collisionData)
        {
            return null;
        }

        public override MovementState OnCollisionExit(CollisionHit collisionDirection, CollisionData collisionData)
        {
            return null;
        }

        public override int GetStateID()
        {
            return 6;
        }
    }
}