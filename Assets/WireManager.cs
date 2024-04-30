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
        if (pictures[0].rotation.z == 0 &&
            pictures[1].rotation.z == 0 &&
            pictures[2].rotation.z == 0 &&
            pictures[3].rotation.z == 0 &&
            pictures[4].rotation.z == 0 &&
            pictures[5].rotation.z == 0 &&
            pictures[6].rotation.z == 0 &&
            pictures[7].rotation.z == 0 &&
            pictures[8].rotation.z == 0 &&
            pictures[9].rotation.z == 0 &&
            pictures[10].rotation.z == 0 &&
            pictures[11].rotation.z == 0 &&
            pictures[12].rotation.z == 0 &&
            pictures[13].rotation.z == 0 &&
            pictures[14].rotation.z == 0 &&
            pictures[15].rotation.z == 0 &&
            pictures[16].rotation.z == 0 &&
            pictures[17].rotation.z == 0 &&
            pictures[18].rotation.z == 0 &&
            pictures[19].rotation.z == 0)
        {
            Won = true;
            winUI.SetActive(true);
        }

    }
}
