using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialoguePanel; // Reference to the UI panel containing the dialogue
    public TMP_Text dialogueText; // Reference to the TextMeshPro text component where the dialogue will be shown
    public string[] dialogues; // Array of dialogues to be displayed
    public float baseLetterDelay = 0.1f; // Base delay between each letter
    public float typingSpeedMultiplier = 1.0f; // Speed multiplier for typing
    public TMP_FontAsset dialogueFont; // Custom font for the dialogue text

    private bool isTyping = false;
    private float nextLetterTime;
    private int currentDialogueIndex;
    private int currentLetterIndex;
    private bool inTrigger = false;
    private bool dialoguesStarted = false; // Indicates if the dialogues have started

    void Start()
    {
        // Hide the dialogue panel initially
        dialoguePanel.SetActive(false);
        // Set the font for the dialogue text
        dialogueText.font = dialogueFont;
    }

    void Update()
    {
        if (dialoguesStarted)
        {
            // Check if currently typing and if it's time to display the next letter
            if (isTyping && Time.time >= nextLetterTime)
            {
                dialogueText.text = dialogues[currentDialogueIndex].Substring(0, currentLetterIndex);
                currentLetterIndex++;

                // If all letters are shown, stop typing
                if (currentLetterIndex > dialogues[currentDialogueIndex].Length)
                {
                    isTyping = false;
                }
                else
                {
                    // Calculate the time for the next letter, considering typing speed multiplier
                    nextLetterTime = Time.time + baseLetterDelay * typingSpeedMultiplier;
                }
            }
        }

        // Check for left mouse button press to advance through dialogues only when player is in the trigger
        if (inTrigger && Input.GetMouseButtonDown(0))
        {
            if (!isTyping && !dialoguesStarted)
            {
                StartDialogue();
            }
            else if (!isTyping && dialoguesStarted)
            {
                currentDialogueIndex++;
                if (currentDialogueIndex < dialogues.Length)
                {
                    StartDialogue();
                }
                else
                {
                    EndDialogue();
                }
            }
            else if (isTyping)
            {
                // If typing, skip to the end of the current dialogue
                SkipTyping();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = true;
            if (!dialoguesStarted)
            {
                StartDialogue();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = false;
            Destroy(gameObject);
            dialoguePanel.gameObject.SetActive(false);
        }
    }

    void StartDialogue()
    {
        // Reset text and index
        dialogueText.text = "";
        currentLetterIndex = 0;

        // Show the dialogue panel
        dialoguePanel.SetActive(true);

        // Start typing the current dialogue
        isTyping = true;
        nextLetterTime = Time.time + baseLetterDelay * typingSpeedMultiplier;

        dialoguesStarted = true;
    }

    void SkipTyping()
    {
        // Skip to the end of the current dialogue
        dialogueText.text = dialogues[currentDialogueIndex];
        currentLetterIndex = dialogues[currentDialogueIndex].Length;
        isTyping = false;
    }

    void EndDialogue()
    {
        // Hide the dialogue panel
        dialoguePanel.SetActive(false);
        dialoguesStarted = false;
        Destroy(gameObject);
    }
}
