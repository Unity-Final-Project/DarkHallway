using UnityEngine;
using UnityEngine.UI;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private bool openTrigger = false;

    // This method will be called when the button is clicked
    public void TriggerDoorOpen()
    {
        if (openTrigger)
        {
            myDoor.Play("DoorOpen", 0, 0.0f);
            gameObject.SetActive(false);
        }
    }
}
