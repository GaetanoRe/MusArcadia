using UnityEngine;
using TMPro;
using System.Collections;

/// <summary>
/// Manages the dialogue system, displaying text lines sequentially and handling UI elements like character names and choices.
/// </summary>
public class DialogueSystem : MonoBehaviour
{
    #region Singleton

    // Singleton instance for accessing the DialogueSystem globally
    public static DialogueSystem instance;

    private void Awake()
    {
        // Ensure there's only one instance of this object
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Preserve the instance between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    #endregion Singleton

    #region UI Elements

    [Header("UI Elements")]
    public TMP_Text dialogueText; // Displays dialogue text

    public TMP_Text TMP_CharName; // Displays the speaker's name
    public GameObject yesBtn; // Yes button for player choices
    public GameObject noBtn; // No button for player choices
    public GameObject closeBtn; // Close button for ending the dialogue

    #endregion UI Elements

    #region References

    // Variables for keeping track of the current NPC and dialogue data
    private string currentNPCName; // Tracks the NPC currently being interacted with

    private GameObject canvasDialogueUI; // Reference to the dialogue UI canvas
    private DialogueScriptsDictionaryComponent dialogueScriptsDictionary; // Reference to the dialogue scripts dictionary

    #endregion References

    #region Initialization

    /// <summary>
    /// Initializes the dialogue system, setting up the UI and references.
    /// </summary>
    private void Start()
    {
        // Locate the dialogue UI canvas in the scene
        canvasDialogueUI = GameObject.Find("CanvasDialogueUI");
        if (canvasDialogueUI != null)
        {
            canvasDialogueUI.SetActive(false); // Hide the dialogue canvas initially
            yesBtn.SetActive(false); // Hide Yes button
            noBtn.SetActive(false); // Hide No button
            closeBtn.SetActive(false); // Hide Close button
        }
        else
        {
            Debug.LogWarning("CanvasDialogueUI not found in the scene!"); // Warning if the canvas is not found
        }

        // Get reference to the dialogue scripts dictionary component
        dialogueScriptsDictionary = GetComponent<DialogueScriptsDictionaryComponent>();
        if (dialogueScriptsDictionary == null)
        {
            Debug.LogError("DialogueScriptsDictionaryComponent not found!"); // Error if the dictionary component is missing
        }
    }

    #endregion Initialization

    #region Dialogue Handling

    /// <summary>
    /// Starts displaying dialogues for a given NPC name.
    /// </summary>
    /// <param name="npcName">The name of the NPC whose dialogues will be displayed.</param>
    public void ShowText(string npcName)
    {
        Debug.Log($"<color=blue>I receive : </color><color=green>{npcName}</color>");

        // Ensure the current NPC name is initialized
        if (currentNPCName == null)
        {
            currentNPCName = npcName;
        }

        currentNPCName = npcName;

        Debug.Log("Hello from Dialogue System! " + npcName);

        // Show the dialogue UI canvas
        if (canvasDialogueUI != null) canvasDialogueUI.SetActive(true);

        // Retrieve the dialogue script for the given NPC
        DialogueScriptSO dialogueScriptSO = dialogueScriptsDictionary.GetDialogueScript(npcName);

        if (dialogueScriptSO == null)
        {
            dialogueText.text = "No dialogue found for this NPC"; // Fallback message if no dialogue is found
            return;
        }

        // Start displaying the dialogues sequentially
        StartCoroutine(DisplayDialoguesAndSpeakerName(dialogueScriptSO));
    }

    /// <summary>
    /// Coroutine to display each dialogue line with a delay.
    /// </summary>
    /// <param name="dialogueScriptSO">The dialogue script containing lines to display.</param>
    private IEnumerator DisplayDialoguesAndSpeakerName(DialogueScriptSO dialogueScriptSO)
    {
        // Loop through each dialogue line in the script
        foreach (var dialogue in dialogueScriptSO.dialogueLines)
        {
            // Display the speaker's name, excluding "onYes" and "onNo"
            string speakerName = dialogue.speakerName;
            speakerName = speakerName.Replace("onYes", "").Replace("onNo", "");
            TMP_CharName.text = speakerName;

            // Display the dialogue text
            dialogueText.text = dialogue.lineText;

            // If it's a question, show the Yes/No buttons
            if (dialogue.isAQuestion)
            {
                yesBtn.SetActive(true);
                noBtn.SetActive(true);
            }
            if (dialogue.isLastDialogueLine)
            {
                closeBtn.SetActive(true); // Show Close button if it's the last dialogue line
            }

            // Wait before displaying the next line
            yield return new WaitForSeconds(3);
        }
    }

    #endregion Dialogue Handling

    #region Button Handling

    /// <summary>
    /// Called when the Yes button is clicked.
    /// </summary>
    public void OnYesBtnClicked()
    {
        ClearConsole(); // Clear the console for cleaner debugging

        Debug.Log($"<color=blue>I send : </color><color=green>onYes{currentNPCName}</color>");

        // Send "onYes" action and restart the dialogue with the updated NPC name
        string _tosend = $"onYes{currentNPCName}";
        ShowText(_tosend);
    }

    /// <summary>
    /// Called when the No button is clicked.
    /// </summary>
    public void OnNoBtnClicked()
    {
        ClearConsole(); // Clear the console for cleaner debugging

        Debug.Log($"<color=blue>I send : </color><color=green>onNo{currentNPCName}</color>");

        // Send "onNo" action and restart the dialogue with the updated NPC name
        string _tosend = $"onNo{currentNPCName}";
        ShowText(_tosend);
    }

    #endregion Button Handling

    #region Utility Methods

    /// <summary>
    /// Clears the Unity console.
    /// </summary>
    public static void ClearConsole()
    {
#if UNITY_EDITOR
        var logEntries = System.Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
        var clearMethod = logEntries.GetMethod("Clear");
        clearMethod.Invoke(new object(), null); // Clear the log entries in the editor
#endif
    }

    #endregion Utility Methods
}