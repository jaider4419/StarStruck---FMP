using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string[] dialogues;
    public GameObject[] objectsToActivate; // Array of GameObjects to activate

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController playerController = other.GetComponent<CharacterController>();
            if (playerController != null)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogues, objectsToActivate, playerController);
            }
            else
            {
                Debug.LogError("CharacterController component not found on the player.");
            }
        }
    }
}
