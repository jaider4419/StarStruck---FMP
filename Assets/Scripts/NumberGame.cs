using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NumberGame : MonoBehaviour
{
    public List<Button> buttons;
    public List<Button> shuffledButtons;
    int counter = 0;
    public string sceneToLoad;
    public GameObject electricBox;
    public bool hasPlayed = false;

    // Start is called before the first frame update
    public void Start()
    {
        StartTheGame();
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartTheGame()
    {
        counter = 0;
        shuffledButtons = buttons.OrderBy(a => Random.Range(0, 100)).ToList();
        for (int i = 1; i < 11; i++)
        {
            shuffledButtons[i - 1].GetComponentInChildren<Text>().text = i.ToString();
            shuffledButtons[i - 1].interactable = true;
            shuffledButtons[i - 1].image.color = new Color32(177, 220, 233, 255);
        }
    }

    public void pressButton(Button button)
    {
        if (int.Parse(button.GetComponentInChildren<Text>().text) - 1 == counter)
        {
            counter++;
            button.interactable = false; 
            button.image.color = Color.green;
            if (counter == 10)
            {
                StartCoroutine(presentResult(true));
                endGame();
            }
        }
        else
        {
            StartCoroutine(presentResult(false));
        }
    }

    public IEnumerator presentResult(bool win)
    {
        if (!win)
        {
            foreach (var button in shuffledButtons)
            {
                button.image.color = Color.red;
                button.interactable = false;
            }
        }

        yield return new WaitForSeconds(2f);
        StartTheGame();
    }

    public void endGame()
    {
        SceneManager.LoadScene(sceneToLoad);
        Destroy(electricBox);
    }

}
