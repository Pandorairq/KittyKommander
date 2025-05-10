using Movement.States;
using UnityEngine;

namespace Reset.InputControllers
{
    public abstract class InputController : MonoBehaviour
    {
        public abstract InputData GetInput();
    }
}
