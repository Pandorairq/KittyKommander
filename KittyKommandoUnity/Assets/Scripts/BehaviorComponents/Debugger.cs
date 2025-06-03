using UnityEngine;

namespace BehaviorComponents
{
    public class Debugger: MonoBehaviour
    {
        [SerializeField] private string message = "Test";
        public void Debug()
        {
            print(message);
        }
    }
}