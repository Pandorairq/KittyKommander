using UnityEngine;

namespace Movement
{
    public class CharacterAnimator : MonoBehaviour
    {
        private Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            if (anim == null)
                Debug.LogError("Animator component missing on this GameObject!");
        }

        public void UpdateAnimationState(Movement.States.MovementState state)
        {
            anim.SetInteger("State", state.GetStateID());
        }
    }
}
