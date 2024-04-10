using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public Transform player; // Player's transform
    public float distance = 5.0f; // Distance from player
    public float height = 2.0f; // Height above player
    public float rotationSpeed = 3.0f; // Speed of camera rotation
    public LayerMask collisionLayers; // Layers to collide with

    private float currentRotation = 0.0f;

    void Start()
    {
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (!player)
            return;

        // Get mouse input for camera rotation
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;

        // Update camera rotation
        currentRotation += mouseX;
        Quaternion rotation = Quaternion.Euler(0, currentRotation, 0);

        // Calculate desired camera position
        Vector3 desiredPosition = player.position - (rotation * Vector3.forward * distance) + Vector3.up * height;

        // Check for camera collision
        RaycastHit hit;
        if (Physics.Linecast(player.position, desiredPosition, out hit, collisionLayers))
        {
            // Adjust camera position if there's a collision
            desiredPosition = hit.point + hit.normal * 0.2f; // Move camera slightly away from collision point
        }

        // Set camera position and rotation
        transform.position = desiredPosition;
        transform.LookAt(player.position);
    }
}






