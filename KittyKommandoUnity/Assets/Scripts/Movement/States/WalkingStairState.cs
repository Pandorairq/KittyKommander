using System;
using Reset;
using UnityEngine;

namespace Movement.States
{
    public class WalkingStairState : GroundedState
    {
        private Vector3 stairDirection;

        public WalkingStairState(Vector3 direction)
        {
            stairDirection = direction;
        }
        public override MovementState HandleInput(MovementComponent movementComponent, InputData inputData)
        {
            if (inputData.JumpInput)
            {
                switch (inputData.HorizontalInput)
                {
                    case >0:
                        return new JumpingState(MoveDirection + Vector3.right, ExternalForce);
                    case <0:
                        return new JumpingState(MoveDirection + Vector3.left, ExternalForce);
                    case 0:
                        return new JumpingState(MoveDirection, ExternalForce);
                }
            }
            switch (inputData.HorizontalInput)
            {
                case >0:
                    MoveDirection += Vector3.right;
                    break;
                case <0:
                    MoveDirection += Vector3.left;
                    break;
                case 0:
                    return null;
            }

            return null;
        }

        public override float Update(MovementComponent movementComponent, float deltaTime)
        {
            var viewDirection = MoveDirection.x;
            movementComponent.Move(stairDirection * MoveDirection.x + ExternalForce);
            MoveDirection = Vector3.zero;
            ExternalForce = Vector3.zero;
            return viewDirection;
        }

        public override void OnStateEnter(MovementComponent movementComponent)
        {
        }

        public override void OnStateExit(MovementComponent movementComponent)
        {
            Vector3 pos = movementComponent.transform.position;
            movementComponent.transform.position = new Vector3(pos.x, MathF.Round(pos.y), pos.z);
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
            return 2;
        }
    }
}