using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
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
        
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material highLightMaterial;
        private SpriteRenderer spriteRenderer;

        public void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Highlight()
        {
            spriteRenderer.material = highLightMaterial;
        }

        public void RemoveHighlight()
        {
            spriteRenderer.material = defaultMaterial;
        }
#if UNITY_EDITOR
        void OnValidate()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            string path = "Assets/Materials/InteractableHighlight.mat";
            if (!defaultMaterial)
            {
                defaultMaterial = spriteRenderer.sharedMaterial;
            }
            if (!highLightMaterial)
            {
                highLightMaterial = AssetDatabase.LoadAssetAtPath<Material>(path);
            }
        }
#endif
    }
}