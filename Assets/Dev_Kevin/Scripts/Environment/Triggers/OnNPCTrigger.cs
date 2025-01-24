using UnityEngine;

/// <summary>
/// This script controls the interaction between a player and an NPC trigger area.
/// When the player enters the trigger, a specific GameObject (e.g., "PressKeyToInteract") is activated.
/// The player can press the "E" key to perform an action when the GameObject is active.
/// </summary>
public class OnNPCTrigger : MonoBehaviour
{
    [HideInInspector] public GameObject dialogueSystem;

    public string selectedNpcName; // The name of the selected NPC
    [HideInInspector] public GameObject interactKeySprite; // Interaction key indicator

    [SerializeField] private float cooldownTime = 0.5f; // Cooldown time for pressing "E"
    private float cooldownTimer = 0f;

    private bool canPressE = false; // Indicates if the player can press "E"
    private bool _inputProcessed = false; // Ensures input isn't processed multiple times

    private void Start()
    {
        // Finds the child GameObject "PressKeyToInteract" and deactivates it initially
        interactKeySprite = transform.Find("PressKeyToInteract")?.gameObject;

        if (interactKeySprite != null)
        {
            interactKeySprite.SetActive(false); // Disable interact indicator
        }
        else
        {
            Debug.LogWarning("PressKeyToInteract not found as a child of " + gameObject.name);
        }
    }

    private void Update()
    {
        // Ensure the DialogueSystem reference is set
        if (dialogueSystem == null)
        {
            dialogueSystem = GameObject.Find("DialogueSystem");
        }

        // Cooldown timer logic
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                _inputProcessed = false; // Resets input processing flag
            }
        }

        // Check for the "E" key press and trigger the interaction logic
        if (Input.GetKeyDown(KeyCode.E) && canPressE && !_inputProcessed)
        {
            if (!GameManager.Instance.GetIsEpressed()) // Access singleton directly
            {
                GameManager.Instance.SetIsEpressed(true); // Update state in GameManager
                _inputProcessed = true;                  // Mark input as processed
                cooldownTimer = cooldownTime;            // Start cooldown timer
                OnTriggerKeyPressed();                   // Execute interaction logic
            }
        }

        // Resets _inputProcessed when the key is released
        if (Input.GetKeyUp(KeyCode.E))
        {
            _inputProcessed = false;
        }
    }

    private void OnTriggerKeyPressed()
    {
        Debug.Log("Key E Pressed!"); // Debug message

        // Example: Disable player movement
        GameObject mainCharacterGO = GameObject.Find("MainCharacter");
        mainCharacterGO.GetComponent<PlayerCtrl>().canPlayerMove = false;

        // Send NPC name to the dialogue system
        dialogueSystem.GetComponent<DialogueSystem>().ShowText(selectedNpcName);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is in the trigger area!");

            // Activate interact indicator and enable "E" interaction
            if (interactKeySprite != null && !interactKeySprite.activeInHierarchy)
            {
                interactKeySprite.SetActive(true);
                canPressE = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left the trigger area!");

            // Deactivate interact indicator and disable "E" interaction
            if (interactKeySprite != null)
            {
                interactKeySprite.SetActive(false);
                canPressE = false;
            }
        }
    }
}