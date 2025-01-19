using UnityEngine;

public class OnNPCTrigger : MonoBehaviour
{
    // Public variable to select the interaction key
    public InteractionKey selectedInteractionKey;

    // Define an enum to select the interaction key from a list
    public enum InteractionKey
    { PressKeyToInteract }

    // Reference to the GameObject for the selected interaction key
    private GameObject interactKey;

    private void Start()
    {
        // Use the selected enum value to find the GameObject
        interactKey = GameObject.Find(selectedInteractionKey.ToString());

        // Check if the GameObject was found
        if (interactKey != null)
        {
            interactKey.SetActive(false); // Initially disable it
        }
        else
        {
            Debug.LogWarning(selectedInteractionKey.ToString() + " not found!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Checks if the object entering the trigger is the one we want
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is in the trigger area!");

            // Enable the GameObject associated with the selected interaction key
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

            // Disable the GameObject associated with the selected interaction key
            if (interactKey != null)
            {
                interactKey.SetActive(false);
            }
        }
    }
}