using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnlockManifolds : MonoBehaviour
{
    public Sprite[] correctSprites; // Array of sprites for correct buttons
    public Button[] buttons; // Array of buttons
    private Sprite[] originalSprites; // Array to store original sprites of buttons
    private List<Vector3> originalPositions = new List<Vector3>(); // List to store original positions of buttons
    public int currentIndex = 0; // Index of the current expected button

    void Start()
    {
        // Store original sprites and positions of buttons
        originalSprites = new Sprite[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            originalSprites[i] = buttons[i].image.sprite;
            originalPositions.Add(buttons[i].transform.position);
            int buttonIndex = i; // Ensure each button has its own index
            buttons[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
        }

        // Randomize button positions
        RandomizeButtonPositions();
    }

    void RandomizeButtonPositions()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int randomIndex = Random.Range(i, buttons.Length);
            // Swap positions
            Vector3 temp = buttons[i].transform.position;
            buttons[i].transform.position = buttons[randomIndex].transform.position;
            buttons[randomIndex].transform.position = temp;
        }
    }

    void OnButtonClick(int buttonIndex)
    {
        // Check if the button pressed is the expected button
        if (buttonIndex == currentIndex)
        {
            // Change button sprite
            buttons[buttonIndex].image.sprite = correctSprites[buttonIndex];
            currentIndex++; // Move to the next expected button

            // Check if all buttons have been pressed
            if (currentIndex >= buttons.Length)
            {
                Debug.Log("Congratulations! You completed the game.");
                // Game completed, you can add further logic here
            }
        }
        else
        {
            // Incorrect button pressed, reset the game
            Debug.Log("Incorrect button pressed. Restarting game.");
            ResetGame();
        }
    }

    void ResetGame()
    {
        // Reset button sprites and positions to original values
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.sprite = originalSprites[i];
            buttons[i].transform.position = originalPositions[i];
        }
        currentIndex = 0; // Reset current index

        // Randomize button positions again
        RandomizeButtonPositions();
    }
}
