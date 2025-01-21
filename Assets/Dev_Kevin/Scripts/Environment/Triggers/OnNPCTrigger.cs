using UnityEngine;

/// <summary>
/// This script controls the interaction between a player and an NPC trigger area.
/// When the player enters the trigger, a specific GameObject (e.g., "PressKeyToInteract") is activated.
/// The player can press the "E" key to perform an action when the GameObject is active.
/// </summary>
public class OnNPCTrigger : MonoBehaviour
{
    [HideInInspector] public GameObject dialogueSystem;

    // Removed the NpcName enum and just use a string for NPC name
    public string selectedNpcName;

    // Reference to the GameObject for the selected interaction key
    [HideInInspector] public GameObject interactKeySprite;

    // Variable to check if the player can press "E" for interaction
    private bool canPressE;

    private void Start()
    {
        // Trouve l'enfant nommé "PressKeyToInteract" parmi les enfants de ce GameObject
        interactKeySprite = transform.Find("PressKeyToInteract")?.gameObject;

        // Si l'enfant est trouvé, désactive-le initialement
        if (interactKeySprite != null)
        {
            interactKeySprite.SetActive(false); // Désactive l'objet
        }
        else
        {
            Debug.LogWarning("PressKeyToInteract not found as a child of " + gameObject.name);
        }

        canPressE = false; // Désactive l'interaction jusqu'à ce que le joueur entre dans la zone de déclenchement
    }

    private void Update()
    {
        if (dialogueSystem == null)
        {
            dialogueSystem = GameObject.Find("DialogueSystem");
        }
        // Check if the "E" key is pressed and if interaction is allowed (i.e., player is inside the trigger area)
        if (Input.GetKeyDown(KeyCode.E) && canPressE == true)
        {
            OnTriggerKeyPressed();
        }
    }

    private void OnTriggerKeyPressed()
    {
        // Print a message to the console when "E" is pressed
        Debug.Log("Key E Pressed !");
        //test
        GameObject maincharacterGO = GameObject.Find("MainCharacter");
        maincharacterGO.GetComponent<PlayerCtrl>().canPlayerMove = false;

        // Send the name of the selected NPC to the ShowText method
        dialogueSystem.GetComponent<DialogueSystem>().ShowText(selectedNpcName);
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the object entering the trigger has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Log the player's entry into the trigger area
            Debug.Log("Player is in the trigger area!");

            // If the interactKey is found and not already active, enable it
            if (interactKeySprite != null && interactKeySprite.activeInHierarchy == false)
            {
                // Enable the GameObject (e.g., "PressKeyToInteract") and allow "E" key press
                interactKeySprite.SetActive(true);
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
            if (interactKeySprite != null)
            {
                interactKeySprite.SetActive(false);
                canPressE = false; // Disable "E" interaction once the player leaves the trigger
            }
        }
    }
}