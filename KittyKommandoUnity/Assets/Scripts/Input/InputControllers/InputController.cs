using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Input.InputControllers
{
    public struct InputData
    {
        public float HorizontalInput;
        public bool Running;
        public bool Jump;
        public bool Action;
    }
    public abstract class InputController : MonoBehaviour
    {
        public UnityEvent<bool> onJumpButton;
        public UnityEvent<bool> onActionButton;
        public UnityEvent<bool> onRunningButton;
        public abstract InputData GetInput();
    }
}
