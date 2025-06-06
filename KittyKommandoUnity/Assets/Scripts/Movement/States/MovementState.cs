using System;
using Input.InputControllers;
using Input.InputControllers.Movement;
using UnityEngine;

namespace Movement.States
{
    [Serializable]
    public abstract class MovementState
    {
        protected float ViewDirection;
        protected Vector3 MoveDirection;
        protected Vector3 ExternalForce;
        public abstract MovementState HandleInput(MovementComponent movementComponent, MovementData movementData);
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
        protected float gravity;

        public override void Update(MovementComponent movementComponent, float deltaTime)
        {
            ViewDirection = MoveDirection.x;
            MoveDirection += Vector3.down * (gravity * deltaTime);
            movementComponent.Move(MoveDirection + ExternalForce);
            MoveDirection.x = 0;
        }
        public Vector3 GetDirection()
        {
            return MoveDirection;
        }
    }

    
    
    

}