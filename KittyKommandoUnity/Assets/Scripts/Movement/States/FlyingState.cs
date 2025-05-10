using Reset;
using UnityEngine;

namespace Movement.States
{
    public class FlyingState : InAirState
    {
        public override MovementState HandleInput(MovementComponent movementComponent, InputData inputData)
        {
            if (inputData.VerticalInput > 0)
            {
                MoveDirection = Vector3.up;
            }
            else
            {
                MoveDirection = Vector3.down;
            }

            return null;
        }
        public override float Update(MovementComponent movementComponent, float deltaTime)
        {
            movementComponent.Move(MoveDirection);
            ExternalForce *= 0.5f;
            return 0;
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
            return 5;
        }
    }
}