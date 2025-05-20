using System;
using Reset;
using UnityEngine;

namespace Movement.States
{
    
    public class StandingState : GroundedState
    {
        public override MovementState HandleInput(MovementComponent movementComponent, InputData inputData)
        {
            
            if (inputData.Jump)
            {
                switch (inputData.HorizontalInput)
                {
                    case >0:
                        return new JumpingState(Vector3.right);
                    case <0:
                        return new JumpingState(Vector3.left);
                    case 0:
                        return new JumpingState(MoveDirection);
                }
            }
            
            switch (inputData.HorizontalInput)
            {
                case >0:
                    return inputData.Running ? new RunningState(Vector3.right) : new WalkingState(Vector3.right);
                case <0:
                    return inputData.Running ? new RunningState(Vector3.left) : new WalkingState(Vector3.left);
                case 0:
                    break;
            }
            
            return null;
        }

        public override void OnStateEnter(MovementComponent movementComponent)
        {
            Vector3 pos = movementComponent.transform.position;
            movementComponent.transform.position = new Vector3(pos.x, MathF.Round(pos.y), pos.z);
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
            if(collisionData.Direction == CollisionDirection.Bottom) return new FallingState(Vector3.zero);
            return null;
        }

        public override int GetStateID()
        {
            return 0;
        }
    }
}