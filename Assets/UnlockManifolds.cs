using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UnlockManifolds : MonoBehaviour
{
    public List<Sprite> images; // List of images to be displayed
    public List<int> correctOrder; // Correct order of images
    public List<Image> imageSlots; // Image slots where images will be displayed
    public GameObject correctImagePrefab; // Prefab of the correct image to display

    private List<int> currentOrder; // Current order of displayed images
    private int currentIndex = 0; // Index to track the player's progress

    void Start()
    {
        // Randomize the initial order of images
        currentOrder = new List<int>();
        for (int i = 0; i < imageSlots.Count; i++)
        {
            int randomIndex = Random.Range(0, images.Count);
            currentOrder.Add(randomIndex);
            imageSlots[i].sprite = images[randomIndex];
        }
    }

    // Function to check if the current order matches the correct order
    void CheckOrder()
    {
        for (int i = 0; i < correctOrder.Count; i++)
        {
            if (currentOrder[i] != correctOrder[i])
            {
                return; // Current order does not match correct order
            }
        }
        // Current order matches correct order
        DisplayCorrectImage();
    }

    // Function to display the correct image
    void DisplayCorrectImage()
    {
        foreach (Image slot in imageSlots)
        {
            slot.enabled = false; // Hide the current images
        }

        Instantiate(correctImagePrefab, transform.position, Quaternion.identity); // Instantiate the correct image
    }

    // Function to handle player input
    public void OnImageClick(int index)
    {
        if (index == correctOrder[currentIndex])
        {
            // Player selected the correct image
            currentIndex++;
            if (currentIndex == correctOrder.Count)
            {
                CheckOrder(); // Check if the current order matches the correct order
            }
        }
        else
        {
            // Player selected the wrong image, reset the sequence
            currentIndex = 0;
        }
    }
}

