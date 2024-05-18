using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueUI;
    public Button nextButton;
    private string[] dialogues;
    private GameObject[] dialogueObjects;
    private int currentDialogueIndex = 0;
    private CharacterController playerController;

    private void Start()
    {
        dialogueUI.SetActive(false);
        nextButton.onClick.AddListener(DisplayNextDialogue);
    }

    public void StartDialogue(string[] dialogueLines, GameObject[] objectsToActivate, CharacterController playerCtrl)
    {
        dialogues = dialogueLines;
        dialogueObjects = objectsToActivate;
        playerController = playerCtrl;

        currentDialogueIndex = 0;
        dialogueUI.SetActive(true);
        dialogueText.text = dialogues[currentDialogueIndex];

        ActivateGameObjectForDialogue(currentDialogueIndex);

        if (playerController != null)
        {
            FreezePlayerMovement(true); // Disable player movement
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void DisplayNextDialogue()
    {
        currentDialogueIndex++;

        if (currentDialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentDialogueIndex];
            ActivateGameObjectForDialogue(currentDialogueIndex);
        }
        else
        {
            EndDialogue();
        }
    }

    private void ActivateGameObjectForDialogue(int index)
    {
        if (dialogueObjects != null)
        {
            for (int i = 0; i < dialogueObjects.Length; i++)
            {
                if (dialogueObjects[i] != null)
                {
                    dialogueObjects[i].SetActive(i == index);
                }
            }
        }
    }

    private void EndDialogue()
    {
        dialogueUI.SetActive(false);

        if (playerController != null)
        {
            FreezePlayerMovement(false); // Enable player movement
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Deactivate all dialogue objects
        if (dialogueObjects != null)
        {
            foreach (var obj in dialogueObjects)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }
    }

    private void FreezePlayerMovement(bool freeze)
    {
        playerController.enabled = !freeze;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && dialogueUI.activeSelf)
        {
            DisplayNextDialogue();
        }
    }
}
