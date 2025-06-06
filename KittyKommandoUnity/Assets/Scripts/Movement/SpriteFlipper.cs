using System;
using Input.InputControllers;
using Input.InputControllers.Movement;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    [SerializeField] private MovementController controller;
    
    private void Update()
    {
        var input = controller.GetInput();
        Flip(input.HorizontalInput);
    }

    private void Flip(float direction)
    {
        if (direction > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
