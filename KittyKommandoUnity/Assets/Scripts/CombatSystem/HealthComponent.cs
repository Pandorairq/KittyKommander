using UnityEngine;

namespace CombatSystem
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int health;

        public void Damage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                print("This unit died");
            }
        }
    }
}