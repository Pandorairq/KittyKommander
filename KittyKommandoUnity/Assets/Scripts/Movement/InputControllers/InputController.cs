using Movement.States;
using UnityEngine;

namespace Movement.InputControllers
{
    public abstract class InputController : MonoBehaviour
    { 
        public abstract InputData GetInput();
    }
}
