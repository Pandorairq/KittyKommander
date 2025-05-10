using Reset;
using UnityEngine;

namespace Movement.States
{
    
    public class RunningState : GroundedState
    {
        public RunningState(Vector3 runningMoveDirection)
        {
            MoveDirection = runningMoveDirection;
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
                    return new StandingState();
            }
            //movementComponent.transform.rotation = Quaternion.Euler(0,InputDirection.x < 0 ? -180 : 0, 0);
            return null;
        }

        public override void OnStateEnter(MovementComponent movementComponent)
        {
        }


        public override void OnStateExit(MovementComponent movementComponent) { }

        public override MovementState OnCollisionEnter(CollisionHit collisionDirection, CollisionData collisionData)
        {
            //if (collisionData.IsStair) return new WalkingStairState();
            return null;
        }

        public override MovementState OnCollisionExit(CollisionHit collisionDirection, CollisionData collisionData)
        { 
            if(collisionDirection == CollisionHit.Bottom) return new FallingState(Vector3.zero);
            return null;
        }

        public override int GetStateID()
        {
            return 1;
        }
    }
}