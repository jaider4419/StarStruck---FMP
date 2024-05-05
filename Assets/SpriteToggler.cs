using UnityEngine;

public class SpriteToggler : MonoBehaviour
{
    public Sprite sprite1; // The first sprite
    public Sprite sprite2; // The second sprite

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Generate a random number to determine the initial sprite position
        bool isSprite1 = Random.Range(0, 2) == 0; // 50% chance of being true or false

        // Set the initial sprite based on the random position
        spriteRenderer.sprite = isSprite1 ? sprite1 : sprite2;
    }

    private void OnMouseDown()
    {
        // Toggle between the two sprites
        spriteRenderer.sprite = (spriteRenderer.sprite == sprite1) ? sprite2 : sprite1;
    }
}
