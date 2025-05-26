using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    // Flip the sprite based on the direction in which it is moving
    public void Flip(Vector2 direction)
    {
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
