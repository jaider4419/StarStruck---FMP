using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] friends;

    private Vector3 playerStartPos;
    private Vector3[] friendsStartPos;

    private void Awake()
    {
        SaveStartingPositions();
        ResetToInitialPositions();
    }

    private void SaveStartingPositions()
    {
        playerStartPos = player.transform.position;
        friendsStartPos = new Vector3[friends.Length];
        for (int i = 0; i < friends.Length; i++)
        {
            friendsStartPos[i] = friends[i].transform.position;
        }
    }

    public void ResetToInitialPositions()
    {
        player.transform.position = playerStartPos;
        for (int i = 0; i < friends.Length; i++)
        {
            friends[i].transform.position = friendsStartPos[i];
        }
    }

    public void SaveCheckpointPositions()
    {
        PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);

        for (int i = 0; i < friends.Length; i++)
        {
            PlayerPrefs.SetFloat("Friend" + i + "PosX", friends[i].transform.position.x);
            PlayerPrefs.SetFloat("Friend" + i + "PosY", friends[i].transform.position.y);
            PlayerPrefs.SetFloat("Friend" + i + "PosZ", friends[i].transform.position.z);
        }

        PlayerPrefs.Save();
    }

    public void LoadCheckpointPositions()
    {
        if (PlayerPrefs.HasKey("PlayerPosX"))
        {
            player.transform.position = new Vector3(
                PlayerPrefs.GetFloat("PlayerPosX"),
                PlayerPrefs.GetFloat("PlayerPosY"),
                PlayerPrefs.GetFloat("PlayerPosZ")
            );
        }

        for (int i = 0; i < friends.Length; i++)
        {
            if (PlayerPrefs.HasKey("Friend" + i + "PosX"))
            {
                friends[i].transform.position = new Vector3(
                    PlayerPrefs.GetFloat("Friend" + i + "PosX"),
                    PlayerPrefs.GetFloat("Friend" + i + "PosY"),
                    PlayerPrefs.GetFloat("Friend" + i + "PosZ")
                );
            }
        }
    }
}
