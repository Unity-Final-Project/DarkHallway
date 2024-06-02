using UnityEngine;

public class LampController : MonoBehaviour
{
    public FirstPersonController Player = null;
    public float activationDistance = 20f;

    void Update()
    {
        if (Player == null)
        {
            Player = FindObjectOfType<FirstPersonController>();
        }

        if (Player == null || gameObject == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

        // Activate/deactivate lamp based on distance to player and current state
        if (!gameObject.activeSelf && distanceToPlayer <= activationDistance)
        {
            gameObject.SetActive(true); // Activate the lamp
            Debug.Log("Lamp activated");
        }
        else
        {
            Debug.Log($"Game Object is " + gameObject.activeSelf + " Is In Range?: " + distanceToPlayer  + activationDistance );
        }

        if (gameObject.activeSelf && distanceToPlayer > activationDistance)
        {
            gameObject.SetActive(false); // Deactivate the lamp
            Debug.Log("Lamp deactivated");
        }
        else
        {
            Debug.Log($"Game Object is " + gameObject.activeSelf + " Is In Range?: " + distanceToPlayer  + activationDistance );
        }
        
    }
}
