using UnityEngine;

public class OnNPCTrigger : MonoBehaviour
{
    // Reference to the "PressKeyToInteract" GameObject
    public GameObject interactKey;

    private void Start()
    {
        interactKey.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        // Checks if the object entering the trigger is the one we want
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is in the trigger area!");

            // Enable the "PressKeyToInteract" GameObject
            if (interactKey != null)
            {
                interactKey.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Checks if the object exiting the trigger is the one we want
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left the trigger area!");

            // Disable the "PressKeyToInteract" GameObject
            if (interactKey != null)
            {
                interactKey.SetActive(false);
            }
        }
    }
}