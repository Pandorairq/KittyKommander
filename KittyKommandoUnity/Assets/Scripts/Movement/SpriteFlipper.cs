using System;
using Movement.InputControllers;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    [SerializeField] private InputController controller;
    
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
