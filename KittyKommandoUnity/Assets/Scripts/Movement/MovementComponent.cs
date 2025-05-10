using System;
using Movement.States;
using Reset.InputControllers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Movement
{
    
    [RequireComponent(typeof(InputController))]
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float jumpHeight;
        [SerializeField] private float inAirSpeed;
        [SerializeField] private float gravity;
        private MovementState state;

        [SerializeField] private InputController inputController;
        [SerializeField] private Rigidbody2D r; 
        [SerializeField] private Animator animator;
        
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
            EvaluateInput();
            var viewDirection = state.Update(this, Time.fixedDeltaTime);
            animator.SetFloat("VerticalDirection", viewDirection);
            print(state);
        }

        private string Test()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Running"))
            {
                return "Player is running right";
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Standing"))
            {
                return "Player is standing right";
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping"))
            {
                return "Player is jumping right";
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Falling"))
            {
                return "Player is Falling right";
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Turning Right"))
            {
                return "Player is turning right";
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Running Mirrored"))
            {
                return "Player is running left";
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Standing Mirrored"))
            {
                return "Player is standing left";
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping Mirrored"))
            {
                return "Player is jumping left";
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Falling Mirrored"))
            {
                return "Player is Falling left";
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Turning left"))
            {
                return "Player is turning left";
            } 
            return "";
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
            r.velocity = new Vector3(direction.x * movementSpeed, direction.y * inAirSpeed, direction.z * movementSpeed);
            //   transform.Translate(new Vector3(direction.x * movementSpeed, direction.y, direction.z * movementSpeed) * deltaTime,Space.World);
        }
    
        public MovementState GetState()
        {
            return state;
        }

        public void CollisionEnter(CollisionHit hit, CollisionData collisionData)
        {
            var s = state.OnCollisionEnter(hit, collisionData);
            SwitchToNewState(s);
        }
        public void CollisionExit(CollisionHit hit, CollisionData collisionData)
        {
            var s = state.OnCollisionExit(hit, collisionData);
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
            animator.SetInteger("PlayerState", s.GetStateID());
            state = s;
            state.OnStateEnter(this);
        }

        public void ResetComponent()
        {
            InitializeState(new StandingState());
        }

        public void SwitchToState(MovementState s)
        {
            if(s is not SwingingState or WalkingStairState) return;
            SwitchToNewState(s);
        }
        
        public void SwitchToDefaultState()
        {
            SwitchToNewState(new StandingState());
        }

    }
}
