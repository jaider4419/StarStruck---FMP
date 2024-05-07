using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimonSaysGame : MonoBehaviour
{
    public GameObject[] greyStarPrefabs; // Array of prefabs for the grey stars
    public GameObject[] yellowStarPrefabs; // Array of prefabs for the yellow stars
    public float displayTime = 1f; // Time each image is displayed
    public float buttonDelay = 0.5f; // Delay between images

    private List<int> sequence = new List<int>(); // Sequence of indexes for stars
    private List<int> playerSequence = new List<int>(); // Player's input sequence
    private int currentIndex = 0; // Index to keep track of the current step in the sequence
    private bool isDisplayingSequence = false; // Flag to indicate whether the sequence is being displayed

    private GameObject[] greyStars; // Array to store grey star objects
    private GameObject[] yellowStars; // Array to store yellow star objects

    void Start()
    {
        // Instantiate grey star objects
        greyStars = new GameObject[9];
        for (int i = 0; i < 9; i++)
        {
            greyStars[i] = Instantiate(greyStarPrefabs[i], transform);
        }

        // Instantiate yellow star objects (offscreen)
        yellowStars = new GameObject[9];
        for (int i = 0; i < 9; i++)
        {
            yellowStars[i] = Instantiate(yellowStarPrefabs[i], transform);
            yellowStars[i].SetActive(false);
        }

        // Generate the sequence for the first level
        GenerateSequence(4);

        // Start the game by showing the sequence
        StartCoroutine(PlaySequence());
    }

    void GenerateSequence(int length)
    {
        sequence.Clear();
        for (int i = 0; i < length; i++)
        {
            sequence.Add(Random.Range(0, 9)); // Generate random indexes for grey stars
        }
    }

    IEnumerator PlaySequence()
    {
        isDisplayingSequence = true;

        // Display the sequence of stars
        foreach (int index in sequence)
        {
            yield return StartCoroutine(DisplayStar(index));
            yield return new WaitForSeconds(displayTime);
            ClearYellowStars();
            yield return new WaitForSeconds(buttonDelay);
        }

        isDisplayingSequence = false;
    }

    IEnumerator DisplayStar(int index)
    {
        // Display yellow star object at the specified index
        yellowStars[index].SetActive(true);
        greyStars[index].SetActive(false);
        yield return null; // Wait for one frame to allow the star to appear
    }

    void ClearYellowStars()
    {
        // Clear the displayed yellow stars
        foreach (var star in yellowStars)
        {
            star.SetActive(false);
        }
        // Show the grey stars again
        foreach (var star in greyStars)
        {
            star.SetActive(true);
        }
    }

    void Update()
    {
        if (!isDisplayingSequence && Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                int index = System.Array.IndexOf(greyStars, hit.collider.gameObject);
                if (index != -1)
                {
                    // Player clicked on a grey star
                    playerSequence.Add(index);
                    CheckSequence();
                }
            }
        }
    }

    void CheckSequence()
    {
        if (playerSequence.Count == sequence.Count)
        {
            bool correctSequence = true;
            for (int i = 0; i < sequence.Count; i++)
            {
                if (playerSequence[i] != sequence[i])
                {
                    correctSequence = false;
                    break;
                }
            }
            if (correctSequence)
            {
                Debug.Log("Correct sequence!");
            }
            else
            {
                Debug.Log("Incorrect sequence! Try again.");
                playerSequence.Clear();
                StartCoroutine(PlaySequence());
            }
        }
    }
}
