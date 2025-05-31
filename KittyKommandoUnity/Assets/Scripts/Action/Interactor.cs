using System;
using Input.InputControllers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Action
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private float interactionRadius;
        [SerializeField] private LayerMask interactionLayer;
        [SerializeField] private Interactable currentInteractable;
        
        //This function has to be passed to the input Controllers Action Event via the inspector
        public void OnActionButton(bool isPressed)
        {
            if (isPressed)
            {
                StartInteractingWithNearestObject();
            }
            else
            {
                StopInteractingWithObject();
            }
        }

        void StartInteractingWithNearestObject()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRadius, interactionLayer);

            Interactable nearest = null;
            float shortestDistance = Mathf.Infinity;

            foreach (Collider2D col in colliders)
            {
                Interactable interactable = col.GetComponent<Interactable>();
                if (interactable != null)
                {
                    float distance = Vector3.Distance(transform.position, col.transform.position);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearest = interactable;
                    }
                }
            }
            
            if (nearest != null)
            {
                currentInteractable = nearest;
                nearest.onInteraction.Invoke(this);
            }
        }

        private void StopInteractingWithObject()
        {
            if(!currentInteractable) return;
            currentInteractable.onRelease.Invoke();
            currentInteractable = null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 1, 0.5f);
            Gizmos.DrawSphere(transform.position, interactionRadius);
        }
    }
}