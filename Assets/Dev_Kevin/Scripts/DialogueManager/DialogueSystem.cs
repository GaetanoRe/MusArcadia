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
    /// Montre les dialogues s�quentiellement pour un NPC donn�.
    /// </summary>
    public void ShowText(string npcName)
    {
        if (currentNPCName == null)
        {
            currentNPCName = npcName;
        }
        Debug.Log("Hello from Dialogue System! " + npcName);
        if (canvasDialogueUI != null) canvasDialogueUI.SetActive(true);

        // R�cup�re le DialogueScriptSO � partir de la cl�
        DialogueScriptSO dialogueScriptSO = dialogueScriptsDictionary.GetDialogueScript(npcName);

        if (dialogueScriptSO == null)
        {
            dialogueText.text = "No dialogue found for this NPC";
            return;
        }

        // Lance la coroutine pour afficher les dialogues s�quentiellement
        StartCoroutine(DisplayDialogues(dialogueScriptSO));
    }

    private string currentNPCName;
    private GameObject canvasDialogueUI; // R�f�rence au Canvas de dialogue

    // R�f�rence au dictionnaire des dialogues
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
        // Recherche du Canvas de dialogue dans la sc�ne
        canvasDialogueUI = GameObject.Find("CanvasDialogueUI");
        if (canvasDialogueUI != null)
        {
            canvasDialogueUI.SetActive(false); // D�sactive le Canvas par d�faut
            yesBtn.SetActive(false);
            noBtn.SetActive(false);
        }
        else
        {
            Debug.LogWarning("CanvasDialogueUI not found in the scene!");
        }

        // R�cup�rer la r�f�rence au DialogueScriptsDictionaryComponent sur le m�me GameObject
        dialogueScriptsDictionary = GetComponent<DialogueScriptsDictionaryComponent>();
        if (dialogueScriptsDictionary == null)
        {
            Debug.LogError("DialogueScriptsDictionaryComponent not found!");
        }
    }

    /// <summary>
    /// Affiche les dialogues un par un avec un d�lai entre chaque ligne.
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
            yield return new WaitForSeconds(3); // Temps d'attente avant de passer � la suivante
        }
    }
}