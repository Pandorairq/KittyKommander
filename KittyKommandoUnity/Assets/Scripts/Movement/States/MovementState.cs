using System;
using Input.InputControllers;

using UnityEngine;

namespace Movement.States
{
    [Serializable]
    public abstract class MovementState
    {
        protected float ViewDirection;
        protected Vector3 MoveDirection;
        protected Vector3 ExternalForce;
        public abstract MovementState HandleInput(MovementComponent movementComponent, InputData inputData);
        public abstract void Update(MovementComponent movementComponent, float deltaTime);
        public abstract void OnStateEnter(MovementComponent movementComponent);
        public abstract void OnStateExit(MovementComponent movementComponent);
        public abstract MovementState OnCollisionEnter(CollisionData collisionData);
        public abstract MovementState OnCollisionExit(CollisionData collisionData);
        public void AddExternalMovement(Vector3 force)
        {
            ExternalForce += force;
        }
        public abstract int GetStateID();
    }

    public abstract class GroundedState : MovementState
    {
        
        public override void Update(MovementComponent movementComponent, float deltaTime)
        {
            ViewDirection = MoveDirection.x;
            movementComponent.Move(MoveDirection + ExternalForce);
            MoveDirection = Vector3.zero;
            ExternalForce = Vector3.zero;
        }
    }

    public abstract class InAirState : MovementState
    {
        protected static bool InAirMovementEnabled = true;
        protected float Gravity;

        public override void Update(MovementComponent movementComponent, float deltaTime)
        {
            ViewDirection = MoveDirection.x;
            MoveDirection += Vector3.down * (Gravity * deltaTime);
            movementComponent.Move(MoveDirection + ExternalForce);
            if(InAirMovementEnabled) MoveDirection.x = 0;
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