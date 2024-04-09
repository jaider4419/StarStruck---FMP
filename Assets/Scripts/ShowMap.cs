using UnityEngine;
using UnityEngine.UI;

public class ShowMap : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public RectTransform mapImage; // Reference to the map image (UI Image)
    public RectTransform playerIcon; // Reference to the player's icon image (UI Image)

    // Size of the map image in world units (e.g., if the map covers 100x100 units in the game world, set this to 100x100)
    public Vector2 mapSize;

    void Start()
    {
        SetMiniMapActive(false); // Initially hide the mini-map
    }

    void Update()
    {
        // Toggle mini-map visibility when pressing the "M" key
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMiniMap();
        }
    }

    void ToggleMiniMap()
    {
        bool isActive = mapImage.gameObject.activeSelf;
        SetMiniMapActive(!isActive);
    }

    void SetMiniMapActive(bool isActive)
    {
        mapImage.gameObject.SetActive(isActive);
        playerIcon.gameObject.SetActive(isActive);
    }

    void LateUpdate()
    {
        if (mapImage.gameObject.activeSelf)
        {
            // Calculate the normalized position of the player on the map
            Vector2 normalizedPosition = new Vector2(
                Mathf.InverseLerp(-mapSize.x / 2f, mapSize.x / 2f, playerTransform.position.x),
                Mathf.InverseLerp(-mapSize.y / 2f, mapSize.y / 2f, playerTransform.position.z)
            );

            // Convert normalized position to the position on the map image
            Vector2 mapPosition = new Vector2(
                Mathf.Lerp(0f, mapImage.rect.width, normalizedPosition.x),
                Mathf.Lerp(0f, mapImage.rect.height, normalizedPosition.y)
            );

            // Set the player's icon position on the map image
            playerIcon.anchoredPosition = mapPosition;
        }
    }
}



