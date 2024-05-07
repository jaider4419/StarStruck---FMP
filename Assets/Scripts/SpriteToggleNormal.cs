
using UnityEngine;

public class SpriteToggleNormal : MonoBehaviour
{
    public Sprite sprite1; // The first sprite
    public Sprite sprite2; // The second sprite

    private SpriteRenderer spriteRenderer;
    private bool isSprite1 = true; // Current state of the sprite

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Set the initial sprite
        spriteRenderer.sprite = sprite1;
    }

    private void OnMouseDown()
    {
        // Toggle between the two sprites
        if (isSprite1)
        {
            spriteRenderer.sprite = sprite2;
        }
        else
        {
            spriteRenderer.sprite = sprite1;
        }
        // Update the state
        isSprite1 = !isSprite1;
    }
}
