using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] pictures;


    public GameObject winUI;

    public static bool Won;


    // Start is called before the first frame update
    void Start()
    {
        winUI.SetActive(false);
        Won = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (
            pictures[2].rotation.z == 90f &&
            pictures[3].rotation.z == 90f &&
            pictures[4].rotation.z == 90f &&
            pictures[5].rotation.z == 90f &&
            pictures[6].rotation.z == 90f &&
            pictures[7].rotation.z == 90f &&
            pictures[8].rotation.z == 90f &&
            pictures[12].rotation.z == 90f &&
            pictures[13].rotation.z == 90f &&
            pictures[16].rotation.z == 90f &&
            pictures[17].rotation.z == 90f)
        {
            Won = true;
            winUI.SetActive(true);
            Debug.Log("you won!");
        }

    }
}
