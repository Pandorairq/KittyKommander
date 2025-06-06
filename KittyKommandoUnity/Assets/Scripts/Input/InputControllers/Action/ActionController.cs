using UnityEngine;
using UnityEngine.Events;

namespace Input.InputControllers.Action
{
    public abstract class ActionController : MonoBehaviour
    {
        public UnityEvent onActionStarted;
        public UnityEvent onActionStopped;
        public UnityEvent onDrop;
    }
}
