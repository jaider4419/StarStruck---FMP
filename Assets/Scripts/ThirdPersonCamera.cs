using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player; // Player transform
    public float distance = 5f; // Distance from the player
    public float height = 2f; // Height from the player
    public float sensitivity = 2f; // Mouse sensitivity
    public LayerMask obstacleMask; // Layer mask to detect obstacles

    private float rotationX = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Camera rotation based on mouse movement
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }

    void LateUpdate()
    {
        // Calculate desired camera position
        Vector3 desiredPosition = player.position - transform.forward * distance + Vector3.up * height;

        // Perform raycast to check for obstacles
        RaycastHit hit;
        if (Physics.Raycast(player.position, desiredPosition - player.position, out hit, distance, obstacleMask))
        {
            // Adjust camera position if there's an obstacle
            transform.position = hit.point;
        }
        else
        {
            // Move camera smoothly to desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.1f);
        }
    }
}



