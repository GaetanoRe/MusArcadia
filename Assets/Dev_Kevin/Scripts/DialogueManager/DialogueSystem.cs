using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance; // Singleton reference

    [Header("UI Elements")]
    public TMP_Text dialogueText; // Le composant UI Text qui affichera les dialogues

    public TMP_Text TMP_CharName; // Affiche le nom du personnage qui parle

    public GameObject yesBtn;
    public GameObject noBtn;

    public void OnYesBtnClicked()
    {
        ShowText("onYes" + currentNPCName);
    }

    public void OnNoBtnClicked()
    {
        ShowText("onNo" + currentNPCName);
    }

    /// <summary>
    /// Montre les dialogues séquentiellement pour un NPC donné.
    /// </summary>
    public void ShowText(string npcName)
    {
        if (currentNPCName == null)
        {
            currentNPCName = npcName;
        }
        Debug.Log("Hello from Dialogue System! " + npcName);
        if (canvasDialogueUI != null) canvasDialogueUI.SetActive(true);

        // Récupère le DialogueScriptSO à partir de la clé
        DialogueScriptSO dialogueScriptSO = dialogueScriptsDictionary.GetDialogueScript(npcName);

        if (dialogueScriptSO == null)
        {
            dialogueText.text = "No dialogue found for this NPC";
            return;
        }

        // Lance la coroutine pour afficher les dialogues séquentiellement
        StartCoroutine(DisplayDialogues(dialogueScriptSO));
    }

    private string currentNPCName;
    private GameObject canvasDialogueUI; // Référence au Canvas de dialogue

    // Référence au dictionnaire des dialogues
    private DialogueScriptsDictionaryComponent dialogueScriptsDictionary;

    private void Awake()
    {
        // Singleton pattern pour s'assurer qu'une seule instance existe
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Recherche du Canvas de dialogue dans la scène
        canvasDialogueUI = GameObject.Find("CanvasDialogueUI");
        if (canvasDialogueUI != null)
        {
            canvasDialogueUI.SetActive(false); // Désactive le Canvas par défaut
            yesBtn.SetActive(false);
            noBtn.SetActive(false);
        }
        else
        {
            Debug.LogWarning("CanvasDialogueUI not found in the scene!");
        }

        // Récupérer la référence au DialogueScriptsDictionaryComponent sur le même GameObject
        dialogueScriptsDictionary = GetComponent<DialogueScriptsDictionaryComponent>();
        if (dialogueScriptsDictionary == null)
        {
            Debug.LogError("DialogueScriptsDictionaryComponent not found!");
        }
    }

    /// <summary>
    /// Affiche les dialogues un par un avec un délai entre chaque ligne.
    /// </summary>
    private IEnumerator DisplayDialogues(DialogueScriptSO dialogueScriptSO)
    {
        foreach (var dialogue in dialogueScriptSO.dialogueLines)
        {
            // Affiche le nom du personnage dans TMP_CharName
            TMP_CharName.text = dialogue.speakerName;

            // Affiche la ligne de dialogue dans l'UI
            dialogueText.text = dialogue.lineText;

            if (dialogue.isAQuestion == true)
            {
                yesBtn.SetActive(true);
                noBtn.SetActive(true);
            }
            yield return new WaitForSeconds(3); // Temps d'attente avant de passer à la suivante
        }
    }
}