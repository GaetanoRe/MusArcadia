using UnityEngine;
using System.Collections.Generic;

public class DialogueScriptsDictionaryComponent : MonoBehaviour
{
    [Header("Dialogue Scripts Dictionary")]
    [Tooltip("Associate a string key with each DialogueScriptSO.")]
    public List<DialogueEntry> dialogues = new List<DialogueEntry>();

    // Exemple d'accès à un ScriptableObject via sa clé
    public DialogueScriptSO GetDialogueScript(string key)
    {
        if (dialogueDictionary.TryGetValue(key, out DialogueScriptSO script))
        {
            return script;
        }
        else
        {
            Debug.LogWarning($"Dialogue script with key '{key}' not found.");
            return null;
        }
    }

    [System.Serializable]
    public class DialogueEntry
    {
        public string key; // Utilise une clé basée sur une chaîne de caractères
        public DialogueScriptSO dialogueScript; // Le ScriptableObject associé
    }

    private Dictionary<string, DialogueScriptSO> dialogueDictionary = new Dictionary<string, DialogueScriptSO>();

    private void Awake()
    {
        // Remplit le dictionnaire à partir de la liste
        foreach (var entry in dialogues)
        {
            if (!string.IsNullOrEmpty(entry.key) && entry.dialogueScript != null)
            {
                if (!dialogueDictionary.ContainsKey(entry.key))
                {
                    dialogueDictionary.Add(entry.key, entry.dialogueScript);
                }
                else
                {
                    Debug.LogWarning($"Duplicate key detected: {entry.key}. Skipping this entry.");
                }
            }
        }
    }
}