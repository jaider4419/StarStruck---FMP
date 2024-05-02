using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NumberGame : MonoBehaviour
{
    public List<GameObject> buttons;
    public List<GameObject> shuffledButtons;
    int counter = 0;
    public string sceneToLoad;
    public GameObject electricBox;

    // Start is called before the first frame update
    public void Start()
    {
        StartTheGame();
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartTheGame()
    {

    }

    private void OnMouseDown()
    {
            transform.Rotate(new Vector3(0, 0, 90));
    }

    public void endGame()
    {
        SceneManager.LoadScene(sceneToLoad);
        Destroy(electricBox);
    }

}
