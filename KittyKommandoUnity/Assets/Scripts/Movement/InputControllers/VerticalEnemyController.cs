using System;
using Movement.InputControllers;
using Movement.States;
using UnityEditor;
using UnityEngine;

namespace Reset.InputControllers
{
    public class VerticalEnemyController : InputController
    {
        private Vector3 startPoint;
        [SerializeField] private Vector3 turnPoint;
        private bool isReturning;
        private float direction;
        void Start()
        {
            startPoint = new Vector3(turnPoint.x, transform.position.y);
            switch (turnPoint.y - startPoint.y)
            {
                case <0:
                    direction = -1;
                    break;
                case >0:
                    direction = 1;
                    break;
                case 0:
                    Debug.LogWarning("The turning Point should not be on the same horizontal than the position");
                    break;
            }

        }

        public void Update()
        {
            if (isReturning)
            {
                if (Math.Abs(transform.position.y - startPoint.y)  <= 0.05f)
                {
                    direction *= -1;
                    isReturning = false;
                }
            }
            else
            {
                if (Math.Abs(transform.position.y - turnPoint.y)  <= 0.05f)
                {
                    direction *= -1;
                    isReturning = true;
                }
            }
        }

        public override InputData GetInput()
        {
            var input = new InputData
            {
                VerticalInput = direction
            };
            return input;
        }

#if UNITY_EDITOR
        public void OnDrawGizmos()
        {
            if (runInEditMode)
            {
                startPoint = transform.position;
            }
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(turnPoint , 0.1f);
            Handles.Label(startPoint, "Start Point");
            Handles.Label(turnPoint, "Turn Point");
        }
#endif
    }
    
}
