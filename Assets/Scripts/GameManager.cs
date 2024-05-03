using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject PipesHolder;
    public GameObject[] Pipes;

    [SerializeField]
    int totalPipes = 0;
    [SerializeField]
    int correctedPipes = 0;

    public GameObject correctui;
    public GameObject WinText;
    public AudioSource win;

    // Start is called before the first frame update
    void Start()
    {
        correctui.SetActive(false);
        WinText.SetActive(false);
        totalPipes = PipesHolder.transform.childCount;

        Pipes = new GameObject[totalPipes];

        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    public void correctMove()
    {
        correctedPipes += 1;

        Debug.Log("correct Move");

        if (correctedPipes == totalPipes)
        {
            Debug.Log("You win!");
            win.Play();
            WinText.SetActive(true);
            correctui.SetActive(true);

            Invoke("ReturnToGame", 2f);
        }

    }

    public void wrongMove()
    {
        correctedPipes -= 1;
    }

    void ReturnToGame()
    {
        SceneManager.LoadScene("First");
    }
}