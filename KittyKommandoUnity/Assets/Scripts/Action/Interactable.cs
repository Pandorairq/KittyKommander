using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Action
{
    [RequireComponent(typeof(Collider2D))]
    public class Interactable : MonoBehaviour
    {
        public UnityEvent<Interactor> onInteraction;
        public UnityEvent onRelease;
    }
}