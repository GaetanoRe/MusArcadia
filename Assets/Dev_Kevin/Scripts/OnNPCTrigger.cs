using UnityEngine;

/// <summary>
/// This script controls the interaction between a player and an NPC trigger area.
/// When the player enters the trigger, a specific GameObject (e.g., "PressKeyToInteract") is activated.
/// The player can press the "E" key to perform an action when the GameObject is active.
/// The interaction key prompt only appears when the player is inside the trigger area.
/// </summary>
public class OnNPCTrigger : MonoBehaviour
{
    // Public variable to select the interaction key
    public InteractionKey selectedInteractionKey;

    // Define an enum to select the interaction key from a list
    public enum InteractionKey
    { PressKeyToInteract }

    // Reference to the GameObject for the selected interaction key
    private GameObject interactKey;

    // Variable to check if the player can press "E" for interaction
    private bool canPressE;

    private void Start()
    {
        // Initialize the interactKey by finding the GameObject with the name specified by the selected enum
        interactKey = GameObject.Find(selectedInteractionKey.ToString());

        // Check if the GameObject was found and set its initial state
        if (interactKey != null)
        {
            interactKey.SetActive(false); // Initially disable it
            canPressE = false;            // Disable "E" interaction until the player enters the trigger
        }
        else
        {
            // Log a warning if the GameObject is not found
            Debug.LogWarning(selectedInteractionKey.ToString() + " not found!");
        }
    }

    private void Update()
    {
        // Check if the "E" key is pressed and if interaction is allowed (i.e., player is inside the trigger area)
        if (Input.GetKeyDown(KeyCode.E) && canPressE == true)
        {
            // Print a message to the console when "E" is pressed
            Debug.Log("Key E Pressed !");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the object entering the trigger has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // If the interactKey is found and not already active, enable it
            if (interactKey != null && interactKey.activeInHierarchy == false)
            {
                // Log the player's entry into the trigger area
                Debug.Log("Player is in the trigger area!");

                // Enable the GameObject (e.g., "PressKeyToInteract") and allow "E" key press
                interactKey.SetActive(true);
                canPressE = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Log the player's exit from the trigger area
            Debug.Log("Player left the trigger area!");

            // Disable the GameObject and prevent "E" key interaction
            if (interactKey != null)
            {
                interactKey.SetActive(false);
                canPressE = false; // Disable "E" interaction once the player leaves the trigger
            }
        }
    }
}