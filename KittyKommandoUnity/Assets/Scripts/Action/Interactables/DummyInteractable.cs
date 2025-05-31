using UnityEngine;

namespace Action.Interactables
{
    public class DummyInteractable: MonoBehaviour
    {
        public void Interact()
        {
            print("Somebody is touching me! :shocking face:");
        }

        public void Release()
        {
            print("Phew, I survived");
        }
    }
}