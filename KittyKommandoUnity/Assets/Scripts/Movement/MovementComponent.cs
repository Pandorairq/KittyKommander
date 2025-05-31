using System;
using Input.InputControllers;
using Movement.States;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Movement
{
    
    [RequireComponent(typeof(InputController))]
    [RequireComponent(typeof(CollisionDetection))]
    public class MovementComponent : MonoBehaviour
    {
        public UnityEvent<MovementState> onStateChanged = new ();
        [SerializeField] private float movementSpeed;
        [SerializeField] private float jumpHeight;
        [SerializeField] private float inAirSpeed;
        [SerializeField] private float gravity;
        private MovementState state;

        [SerializeField] private InputController inputController;
        [SerializeField] private CollisionDetection collisionDetection;
        [SerializeField] private Rigidbody2D r;

        void Start()
        {
            ResetComponent();
            r = GetComponent<Rigidbody2D>();
        }

        public void EvaluateInput()
        {
            var inputData = inputController.GetInput();
            var s = state.HandleInput(this, inputData);
            SwitchToNewState(s);
        }

        void FixedUpdate()
        {
            collisionDetection.CheckCollisions();
            EvaluateInput();
            state.Update(this, Time.fixedDeltaTime);
        }
        
        
        public bool IsGrounded()
        {
            return state is GroundedState;
        }
        public float GetJumpHeight()
        {
            return jumpHeight;
        }
        public float GetGravity()
        {
            return gravity;
        }
        public void Move(Vector3 direction)
        {
            //r.linearVelocity = new Vector3(direction.x * movementSpeed, direction.y * inAirSpeed, direction.z * movementSpeed);
            r.MovePosition(r.position + new Vector2(direction.x * movementSpeed, direction.y * inAirSpeed));
            //   transform.Translate(new Vector3(direction.x * movementSpeed, direction.y, direction.z * movementSpeed) * deltaTime,Space.World);
        }
    
        public MovementState GetState()
        {
            return state;
        }

        public void CollisionEnter(CollisionData collisionData)
        {
            var s = state.OnCollisionEnter(collisionData);
            SwitchToNewState(s);
        }
        public void CollisionExit(CollisionData collisionData)
        {
            var s = state.OnCollisionExit(collisionData);
            SwitchToNewState(s);
        }
        public bool HasState(Type stateType)
        {
            return stateType.IsInstanceOfType(state);
        }

        private void SwitchToNewState(MovementState s)
        {
            if (s != null)
            {
                state.OnStateExit(this);
                InitializeState(s);
            }
        }

        private void InitializeState(MovementState s)
        {
            state = s;
            state.OnStateEnter(this);
            onStateChanged.Invoke(state);
        }

        public void ResetComponent()
        {
            InitializeState(new StandingState());
        }

        private void OnEnable()
        {
            collisionDetection.collisionEnter.AddListener(CollisionEnter);
            collisionDetection.collisionExit.AddListener(CollisionExit);
        }

        private void OnDisable()
        {
            collisionDetection.collisionEnter.AddListener(CollisionEnter);
            collisionDetection.collisionExit.AddListener(CollisionExit);
        }
    }
}
