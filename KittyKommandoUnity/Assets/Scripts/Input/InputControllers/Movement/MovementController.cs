using UnityEngine;
using UnityEngine.Events;

namespace Input.InputControllers.Movement
{
    public struct MovementData
    {
        public float HorizontalInput;
        public bool Running;
        public bool Jump;
    }
    public abstract class MovementController : MonoBehaviour
    {
        public abstract MovementData GetInput();
    }
}
