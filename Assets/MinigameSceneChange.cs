using UnityEngine;

public class MinigameSceneChange : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private string sceneToLoad;
    [SerializeField] private GameObject toDestroy;

    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Leaving the city.");

        // Save player position before leaving the city
        GameObject gameManager = GameObject.Find("GameManager2");
        if (gameManager != null)
        {
            gameManager.GetComponent<GameManager2>().SavePlayerPosition();
        }

        // Load the minigame scene
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            gameManager.GetComponent<GameManager2>().LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("No scene specified to load.");
        }

        // Optionally destroy the specified GameObject
        if (toDestroy != null)
        {
            Destroy(toDestroy);
        }

        return true;
    }
}
