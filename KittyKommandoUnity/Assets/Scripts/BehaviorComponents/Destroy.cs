using UnityEngine;

namespace BehaviorComponents
{
    public class DestroyGameObject : MonoBehaviour
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}