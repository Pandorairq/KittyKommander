using System;
using UnityEngine;

namespace Input.InputControllers
{
    public class PlayerInputActionsWrapper : MonoBehaviour
    {
        private PlayerInputActions inputActions;
        public PlayerInputActions GetPlayerInputActions()
        {
            return inputActions ??= new PlayerInputActions();
        }
    }
}