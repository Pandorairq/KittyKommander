using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Action
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private float interactionRadius;
        [SerializeField] private LayerMask interactionLayer;
        [SerializeField] private Interactable currentInteractable;
        private List<Interactable> interactables = new ();


        private void Update()
        {
            Interactable nearest = null;
            float shortestDistance = Mathf.Infinity;

            foreach (Interactable interactable in interactables)
            {
                if (!interactable) continue;
                float distance = Vector3.Distance(transform.position, interactable.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearest = interactable;
                }
            }

            currentInteractable?.RemoveHighlight();
            currentInteractable = nearest;
            currentInteractable?.Highlight();
            
        }

        //This function has to be passed to the input Controllers Action Event via the inspector
        public void OnActionStarted()
        { 
            StartInteractingWithNearestObject();
        }

        //This function has to be passed to the input Controllers Action Event via the inspector
        public void OnActionStopped()
        {
            StopInteractingWithObject();
        }

        void StartInteractingWithNearestObject()
        {
            if (currentInteractable)
            {
                currentInteractable.onInteraction.Invoke(this);
            }
        }

        private void StopInteractingWithObject()
        {
            if(!currentInteractable) return;
            currentInteractable.onRelease.Invoke();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            var interactable = other.GetComponent<Interactable>();
            print("test");
            if (interactable)
            {
                print("Hi");
                interactables.Add(interactable);
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            var interactable = other.GetComponent<Interactable>();
            if (interactable)
            {
                interactables.Remove(interactable);
            }
            
        }
    }
}