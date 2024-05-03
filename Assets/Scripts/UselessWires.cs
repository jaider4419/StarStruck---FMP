using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UselessWires : MonoBehaviour
{
    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, 90));
    }

}
