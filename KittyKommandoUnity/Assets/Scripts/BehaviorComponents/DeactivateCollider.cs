using UnityEngine;

namespace BehaviorComponents
{
    [RequireComponent(typeof(Collider2D))]
    public class DeactivateCollider : MonoBehaviour
    {
        private Collider2D colliderToDeactivate;
        public void Start()
        {
            colliderToDeactivate = GetComponent<Collider2D>();
        }

        public void Deactivate()
        {
            colliderToDeactivate.enabled = false;
        }
    }
}
