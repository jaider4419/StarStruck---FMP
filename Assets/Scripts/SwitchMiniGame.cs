using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchMiniGame : MonoBehaviour
{
    public Image[,] switchImages; // 2D array of switch images
    public Sprite switchOnSprite; // Sprite for the switch when it's on
    public Sprite switchOffSprite; // Sprite for the switch when it's off

    public Image correctnessImage; // UI image to display correctness
    public Sprite[] correctnessSprites; // Array of sprites to display correctness (e.g., "0 correct", "1 correct", ..., "10 correct")

    private bool[,] switchStates; // 2D array to store switch states
    private bool[,] switchCorrectness; // 2D array to store switch correctness

    private int correctSwitchCount; // Number of correct switches

    // Start is called before the first frame update
    void Start()
    {
        // Initialize switch states
        switchStates = new bool[switchImages.GetLength(0), switchImages.GetLength(1)];
        switchCorrectness = new bool[switchImages.GetLength(0), switchImages.GetLength(1)];

        // Example: Set some switches as correct (true) and others as incorrect (false)
        // You can customize this according to your game's logic
        switchCorrectness[0, 0] = true; // Example: First switch is correct
        switchCorrectness[1, 1] = true; // Example: Second switch is correct
        // Set other switches as incorrect by default

        // Initialize switch images and correctness
        for (int i = 0; i < switchImages.GetLength(0); i++)
        {
            for (int j = 0; j < switchImages.GetLength(1); j++)
            {
                switchStates[i, j] = false; // Initially, all switches are off
                switchImages[i, j].sprite = switchOffSprite; // Set initial switch image
                UpdateSwitchImage(i, j); // Update switch image based on correctness
            }
        }

        // Update correctness image to show initial correct switch count
        UpdateCorrectnessImage();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for player input (touch or click) on switches
        for (int i = 0; i < switchImages.GetLength(0); i++)
        {
            for (int j = 0; j < switchImages.GetLength(1); j++)
            {
                if (Input.GetMouseButtonDown(0)) // Assuming left mouse button for input
                {
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                    if (hit.collider != null && hit.collider.gameObject == switchImages[i, j].gameObject)
                    {
                        // Toggle switch state
                        switchStates[i, j] = !switchStates[i, j];
                        // Update switch image
                        UpdateSwitchImage(i, j);
                        // Update correctness image
                        UpdateCorrectnessImage();
                    }
                }
            }
        }
    }

    // Update switch image based on correctness
    void UpdateSwitchImage(int row, int col)
    {
        switchImages[row, col].sprite = switchStates[row, col] ? switchOnSprite : switchOffSprite;
    }

    // Update correctness image based on correct switch count
    void UpdateCorrectnessImage()
    {
        // Count the number of correct switches
        correctSwitchCount = CountCorrectSwitches();

        // Update the correctness image sprite based on the correct switch count
        correctnessImage.sprite = correctnessSprites[Mathf.Clamp(correctSwitchCount, 0, correctnessSprites.Length - 1)];

        // Check if the player has achieved the correct number of correct switches
        if (correctSwitchCount == switchImages.GetLength(1))
        {
            Debug.Log("Congratulations! You've found the correct combination!");
            // Optionally, you can add code here to handle winning the game
        }
    }

    // Count the number of correct switches
    int CountCorrectSwitches()
    {
        int count = 0;
        for (int i = 0; i < switchImages.GetLength(0); i++)
        {
            for (int j = 0; j < switchImages.GetLength(1); j++)
            {
                if (switchStates[i, j] && switchCorrectness[i, j])
                {
                    count++;
                }
            }
        }
        return count;
    }
}
