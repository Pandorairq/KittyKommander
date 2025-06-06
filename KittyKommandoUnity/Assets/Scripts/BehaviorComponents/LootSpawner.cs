using UnityEngine;

namespace BehaviorComponents
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private float lootSpeed = 5f;
        [SerializeField] private GameObject[] loot;
        [SerializeField] private bool destroyAfterSpawning;

        public void SpawnLoot()
        {
            foreach (GameObject l in loot)
            {
                var lootObject = Instantiate(l, transform.position, Quaternion.identity);
                var lootRigidBody = lootObject.GetComponent<Rigidbody2D>();
                if (lootRigidBody)
                {
                    lootRigidBody.linearVelocity = Vector2.up * lootSpeed;
                }
            }
        }
    }
}