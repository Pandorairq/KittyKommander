using System;
using UnityEngine;
using UnityEngine.Events;

namespace Collectibles
{
    public class Collectable : MonoBehaviour
    {
        public UnityEvent onCollected;
        public LayerMask collectorLayers = 8;
        public void OnTriggerEnter2D(Collider2D other)
        {
            // Layer vom anderen GameObject ist nicht in collectorLayers
            if (((1 << other.gameObject.layer) & collectorLayers.value) == 0) return;
            
            onCollected.Invoke();
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            // Layer vom anderen GameObject ist nicht in collectorLayers
            if (((1 << other.gameObject.layer) & collectorLayers.value) == 0) return;
            
            onCollected.Invoke();
        }
    }
}
