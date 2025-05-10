using System;
using Movement.States;
using TriggerTiles;

namespace Reset.InputControllers
{
    public class KongController : InputController
    {
        private InputData currentInput;

        public void Start()
        {
            currentInput = new InputData();
            //LeverTile.OnLeverTriggered.AddListener(OnLeverTriggered);
        }

        public override InputData GetInput()
        {
            return currentInput;
        }


        private void OnLeverTriggered(ILeverTrigger lever)
        {
            if (lever is KongLever)
            {
                currentInput.JumpInput = true;
            }
        }
    }
}