using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public Camera minimapCamera; // Reference to the minimap camera
    private FirstPersonController Player;
    public Vector3 offset = new Vector3(0, 50, 0); // Offset from the player's position

    // Define the map boundaries
    public float mapMinX = -1f; // Minimum X coordinate
    public float mapMaxX = 1f;  // Maximum X coordinate
    public float mapMinZ = -1f; // Minimum Z coordinate
    public float mapMaxZ = 1f;  // Maximum Z coordinate

    void Awake()
    {
        if (minimapCamera == null)
        {
            minimapCamera = GetComponent<Camera>();
        }
        Player = FindObjectOfType<FirstPersonController>();

        if (minimapCamera != null)
        {
            // Get the bitmask of the "Enemy" layer
            int enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
            // Get the bitmask of the "Ceiling" layer
            int ceilingLayerMask = 1 << LayerMask.NameToLayer("Ceiling");

            // Combine the bitmasks to exclude both "Enemy" and "Ceiling" layers
            int combinedMask = enemyLayerMask | ceilingLayerMask;

            // Invert the combined bitmask to exclude both "Enemy" and "Ceiling" layers
            minimapCamera.cullingMask &= ~combinedMask;

            if (Player != null)
            {
                // Set the minimap camera's initial position to the player's position with an offset
                minimapCamera.transform.position = ClampPosition(Player.transform.position);
                // Set the minimap camera's rotation to look straight down
                minimapCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
            }
            else
            {
                Debug.LogError("Player object is missing!");
            }
        }
        else
        {
            Debug.LogError("Minimap Camera component is missing!");
        }
    }

    void Update()
    {
        if (Player == null)
        {
            Player = FindObjectOfType<FirstPersonController>();
        }
        else
        {
            // Set the minimap camera's position to follow the player with an offset, and clamp it within the map boundaries
            minimapCamera.transform.position = ClampPosition(Player.transform.position + offset);
        }
    }

    Vector3 ClampPosition(Vector3 targetPosition)
    {
        float clampedX = Mathf.Clamp(targetPosition.x, mapMinX, mapMaxX);
        float clampedZ = Mathf.Clamp(targetPosition.z, mapMinZ, mapMaxZ);
        return new Vector3(clampedX, targetPosition.y, clampedZ);
    }
}