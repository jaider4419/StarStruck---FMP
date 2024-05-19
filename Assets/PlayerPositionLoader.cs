using UnityEngine;

public class PlayerPositionLoader : MonoBehaviour
{
    public Vector3 defaultStartingPosition;

    void Start()
    {
        LoadPlayerPosition();
    }

    private void LoadPlayerPosition()
    {
        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");
            transform.position = new Vector3(x, y, z);
        }
        else
        {
            transform.position = defaultStartingPosition;
        }
    }
}
