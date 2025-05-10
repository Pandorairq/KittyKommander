using Movement.States;
using Rewired;
using UnityEngine;

namespace Reset.InputControllers
{
    public class PlayerController : InputController
    {
        [SerializeField] private int playerID = 0;
        private Player player;

        public override InputData GetInput()
        {
            var input = new InputData
            {
                HorizontalInput = player.GetAxis("Move Horizontal"),
                JumpInput = player.GetButton("Jump")
            };
            return input;
        }

        void Start()
        {
            player = ReInput.players.GetPlayer(playerID);
        }

    }
}
