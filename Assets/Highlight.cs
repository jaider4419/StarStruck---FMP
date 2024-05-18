using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public string retryScene;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QuitButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(retryScene);
    }

}
