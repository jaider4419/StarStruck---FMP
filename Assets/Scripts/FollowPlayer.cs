using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 5f;
    public float stoppingDistance = 2f;

    private Vector3 lastPlayerPosition;

    void Start()
    {
        // Record initial player position
        lastPlayerPosition = player.position;
    }

    void Update()
    {
        // Calculate the distance between friend and player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is moving
        if (Vector3.Distance(player.position, lastPlayerPosition) > 0.01f)
        {
            // Move towards the player if they are moving
            if (distanceToPlayer > stoppingDistance)
            {
                Vector3 direction = player.position - transform.position;
                direction.y = 0; // ensure the friend doesn't move up or down
                transform.position += direction.normalized * followSpeed * Time.deltaTime;
            }
        }

        // Update last player position
        lastPlayerPosition = player.position;
    }
}

