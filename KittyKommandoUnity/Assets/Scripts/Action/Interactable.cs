using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Action
{
    public class Interactable : MonoBehaviour
    {
        public UnityEvent<Interactor> onInteraction;
        public UnityEvent onRelease;
    }
}