using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameSceneChange : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    public string sceneToLoad;
    public string InteractionPrompt => prompt;
    public GameObject toDestroy;
    public PlayerManager playerManager;

    public void TransitionToMinigame()
    {
        playerManager.SaveCheckpointPositions(); // Save checkpoint positions before transitioning
        SceneManager.LoadScene(sceneToLoad);
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Changing Scene.");
        TransitionToMinigame();
        return true;
    }
}
