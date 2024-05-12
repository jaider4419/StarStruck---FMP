using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimonGameManager : MonoBehaviour
{
    public List<Sprite> patternSprites; // List to store the generated pattern sprites (yellow stars)
    public List<SpriteRenderer> starRenderers; // List of SpriteRenderers representing the star buttons

    private List<Sprite> offSprites; // List to store the "off" state sprites (grey stars)
    private int currentIndex; // Index to keep track of the current element in the pattern
    private bool showingPattern; // Flag to indicate if the pattern is currently being shown

    void Start()
    {
        InitializeOffSprites();
        StartGame();
    }

    void InitializeOffSprites()
    {
        offSprites = new List<Sprite>();
        foreach (var renderer in starRenderers)
        {
            offSprites.Add(renderer.sprite);
        }
    }

    void StartGame()
    {
        currentIndex = 0;
        showingPattern = true;

        // Generate initial pattern (you can implement your own logic here)
        GeneratePattern();
        ShowPattern();
    }

    void GeneratePattern()
    {
        // Add random sprites to the pattern (you can customize this logic)
        for (int i = 0; i < 4; i++)
        {
            patternSprites.Add(GetRandomSprite());
        }
    }

    Sprite GetRandomSprite()
    {
        // Return a random sprite (you can customize this logic)
        return null;
    }

    void ShowPattern()
    {
        StartCoroutine(ShowPatternCoroutine());
    }

    IEnumerator ShowPatternCoroutine()
    {
        // Display each sprite in the pattern with a delay between them
        foreach (Sprite sprite in patternSprites)
        {
            // Display sprite (you can implement your own logic here)
            starRenderers[currentIndex].sprite = sprite;
            yield return new WaitForSeconds(0.5f); // Show sprite for 0.5 seconds
            starRenderers[currentIndex].sprite = offSprites[currentIndex]; // Turn off sprite
            currentIndex++;
        }

        // After showing the pattern, reset the star buttons to their "off" state
        ResetStarButtons();
        showingPattern = false;
    }

    void ResetStarButtons()
    {
        for (int i = 0; i < starRenderers.Count; i++)
        {
            starRenderers[i].sprite = offSprites[i];
        }
    }

    void CheckPlayerInput(Sprite inputSprite)
    {
        if (showingPattern)
        {
            // Ignore player input during pattern display phase
            return;
        }

        if (inputSprite == patternSprites[currentIndex])
        {
            // Correct input
            currentIndex++;

            if (currentIndex >= patternSprites.Count)
            {
                // Player completed the pattern, they win
                Debug.Log("You win!");
                StartGame(); // Start a new game
            }
        }
        else
        {
            // Incorrect input, game over
            Debug.Log("Game Over");
            // Restart the pattern display
            currentIndex = 0;
            showingPattern = true;
            StartCoroutine(ShowPatternCoroutine());
        }
    }

    // Method to be called by buttons representing player input
    public void OnPlayerInput(Sprite inputSprite)
    {
        CheckPlayerInput(inputSprite);
    }
}
