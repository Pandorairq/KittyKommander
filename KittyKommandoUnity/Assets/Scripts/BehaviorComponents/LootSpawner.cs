using UnityEngine;

namespace BehaviorComponents
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] loot;
        [SerializeField] private bool destroyAfterSpawning;

        public void SpawnLoot()
        {
            foreach (GameObject l in loot)
            {
                Instantiate(l, transform.position, Quaternion.identity);
            }
        }
    }
}