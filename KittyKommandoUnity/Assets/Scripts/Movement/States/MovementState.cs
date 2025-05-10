using System;
using Reset;
using UnityEngine;

namespace Movement.States
{
    public struct InputData
    {
        public float VerticalInput;
        public float HorizontalInput;
        public bool JumpInput;
    }
    [Serializable]
    public abstract class MovementState
    {
        protected Vector3 MoveDirection;
        protected Vector3 ExternalForce;
        public abstract MovementState HandleInput(MovementComponent movementComponent, InputData inputData);
        public abstract float Update(MovementComponent movementComponent, float deltaTime);
        public abstract void OnStateEnter(MovementComponent movementComponent);
        public abstract void OnStateExit(MovementComponent movementComponent);
        public abstract MovementState OnCollisionEnter(CollisionHit collisionDirection, CollisionData collisionData);
        public abstract MovementState OnCollisionExit(CollisionHit collisionDirection, CollisionData collisionData);
        public void AddExternalMovement(Vector3 force)
        {
            ExternalForce += force;
        }
        public abstract int GetStateID();
    }

    public abstract class GroundedState : MovementState
    {
        
        public override float Update(MovementComponent movementComponent, float deltaTime)
        {
            var viewDirection = MoveDirection.x;
            movementComponent.Move(MoveDirection + ExternalForce);
            MoveDirection = Vector3.zero;
            ExternalForce = Vector3.zero;
            return viewDirection;
        }
    }

    public abstract class InAirState : MovementState
    {
        protected static bool InAirMovementEnabled = true;
        protected float Gravity;

        public override float Update(MovementComponent movementComponent, float deltaTime)
        {
            var viewDirection = MoveDirection.x;
            MoveDirection += Vector3.down * (Gravity * deltaTime);
            movementComponent.Move(MoveDirection + ExternalForce);
            if(InAirMovementEnabled) MoveDirection.x = 0;
            return viewDirection;
        }
        public Vector3 GetDirection()
        {
            return MoveDirection;
        }

        public static void SetInAirMovement(bool enabled)
        {
            InAirMovementEnabled = enabled;
        }
    }

    
    
    

}