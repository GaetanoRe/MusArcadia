using UnityEngine;
using System.Collections.Generic;

public class DialogueScriptsDictionaryComponent : MonoBehaviour
{
    [Header("Dialogue Scripts Dictionary")]
    [Tooltip("Associate a string key with each DialogueScriptSO.")]
    public List<DialogueEntry> dialogues = new List<DialogueEntry>();

    // Exemple d'acc�s � un ScriptableObject via sa cl�
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
        public string key; // Utilise une cl� bas�e sur une cha�ne de caract�res
        public DialogueScriptSO dialogueScript; // Le ScriptableObject associ�
    }

    private Dictionary<string, DialogueScriptSO> dialogueDictionary = new Dictionary<string, DialogueScriptSO>();

    private void Awake()
    {
        // Remplit le dictionnaire � partir de la liste
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