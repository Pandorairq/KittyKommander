using System;
using UnityEngine;
using UnityEngine.Events;

namespace Collectibles
{
    public class Collectable : MonoBehaviour
    {
        public UnityEvent onCollected;
        public LayerMask collectorLayer;
        public void OnTriggerEnter2D(Collider2D other)
        {
            onCollected.Invoke();
        }
    }
}
