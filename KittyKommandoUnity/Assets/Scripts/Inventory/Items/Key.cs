using UnityEngine;

namespace BehaviorComponents
{
    public enum KeyColor
    {
        Red,
        Green,
        Blue
    }
    public class Key : MonoBehaviour
    {
        [SerializeField] private KeyColor keyColor;

        public KeyColor GetKeyColor()
        {
            return keyColor;
        }
    }
}
