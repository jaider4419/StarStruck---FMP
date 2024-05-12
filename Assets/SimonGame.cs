using UnityEngine;

public class SimonGame : MonoBehaviour
{
    public Sprite offSprite; // Sprite for the "off" state (grey star)
    public Sprite onSprite; // Sprite for the "on" state (yellow star)

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = offSprite; // Initialize star button to "off" state
    }

    // Method to be called when the button is clicked
    void OnMouseDown()
    {
        // Notify the game manager of player input
        FindObjectOfType<SimonGameManager>().OnPlayerInput(onSprite);
    }
}
