using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    public string sceneName;
    private bool dialogueShown = false; // Flag to track if dialogue has been shown
    private DialogueManager dialogueManager;

    [SerializeField] private string[] dialogues; // Array to hold dialogue lines

    public string InteractionPrompt => prompt;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>(); // Find the DialogueManager in the scene
        if (dialogueManager == null)
        {
            Debug.LogError("DialogueManager not found in the scene!");
        }
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Opening dialogue!");
        if (!dialogueShown) // Show dialogue only if it hasn't been shown before
        {
            ShowDialogue();
            dialogueShown = true;
        }
        return true;
    }

    public void loadSomething()
    {
        SceneManager.LoadScene(sceneName);
    }

    private void ShowDialogue()
    {
        if (dialogueManager != null)
        {
            if (dialogues != null && dialogues.Length > 0)
            {
                // Start the dialogue
                dialogueManager.StartDialogue(dialogues); // Only pass dialogues
            }
            else
            {
                Debug.LogError("No dialogue lines set!");
            }
        }
        else
        {
            Debug.LogError("DialogueManager not found!");
        }
    }
}
