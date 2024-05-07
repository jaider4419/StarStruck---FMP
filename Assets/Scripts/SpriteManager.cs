using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public SpriteRenderer[] switchRenderers; // Array to hold references to the SpriteRenderer components of all the switches

    public Sprite[] correctSequence; // Array to define the correct sequence of sprites for winning the game


    private bool hasWon = false; // Flag to track whether the win condition has been triggered

    void Start()
    {

    }

    void Update()
    {
        // Check if the win condition is met and the message hasn't been logged yet
        if (!hasWon && CheckWinCondition())
        {
            Debug.Log("You win!");

            hasWon = true; // Set the flag to indicate that the win condition has been triggered
        }
    }

    private bool CheckWinCondition()
    {
        // Check if the current sequence of sprites matches the correct sequence
        for (int i = 0; i < switchRenderers.Length; i++)
        {
            if (switchRenderers[i].sprite != correctSequence[i])
            {
                // If any switch is not in the correct state, return false
                return false;
            }
        }
        // If all switches are in the correct state, return true
        return true;
    }
}
