using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bufferloader : MonoBehaviour, IInteractable
{



    [SerializeField] private string prompt;
    public string sceneName;

    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Opening chest!");
        loadSomething();
        return true;
    }

    public void loadSomething()
    {
        SceneManager.LoadScene(sceneName);
    }


}
