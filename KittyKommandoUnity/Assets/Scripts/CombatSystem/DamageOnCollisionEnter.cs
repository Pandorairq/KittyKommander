using UnityEngine;

namespace CombatSystem
{
    public class DamageOnCollisionEnter : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private LayerMask layersToHit;
        public void OnCollisionEnter2D(Collision2D other)
        {
            if (((1 << other.gameObject.layer) & layersToHit.value) == 0) return;
            var healthComponent = other.gameObject.GetComponent<HealthComponent>();
            healthComponent.Damage(damage);
            print("I have hit something!");
        }
    }
}