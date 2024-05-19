using UnityEngine;

public class PlayerPositionSaver : MonoBehaviour
{
    public void SavePlayerPosition()
    {
        Vector3 position = transform.position;
        PlayerPrefs.SetFloat("PlayerX", position.x);
        PlayerPrefs.SetFloat("PlayerY", position.y);
        PlayerPrefs.SetFloat("PlayerZ", position.z);
        PlayerPrefs.Save();
    }
}
