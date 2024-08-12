using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadGameAfterDelay : MonoBehaviour
{
    public string sceneName; 
    public float delayInSeconds = 5f; 

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndLoadScene());
    }

    private IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(sceneName);
    }
}
