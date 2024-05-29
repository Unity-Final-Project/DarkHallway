using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    // Speed of the rotation
    public float rotationSpeed = 100f;

    void Update()
    {
        // Rotate the coin around its Y axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    // This method is called when another collider enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        // Check if the collider that entered is the player
        if (other.CompareTag("Player"))
        {
            // Destroy the coin GameObject
            Destroy(gameObject);
        }
    }
}
