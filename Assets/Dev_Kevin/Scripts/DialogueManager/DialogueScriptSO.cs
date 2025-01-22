using UnityEngine;

#if UNITY_EDITOR

using UnityEditor; // N�cessaire pour le CustomEditor

#endif

[CreateAssetMenu(fileName = "NewDialogueScript", menuName = "Dialogue/DialogueScriptSO")]
public class DialogueScriptSO : ScriptableObject
{
    [Header("List of characters involved in the dialogue")]
    public string[] characterNames = new string[] { "Player" }; // Initialisation avec un personnage par d�faut

    [System.Serializable]
    public class DialogueLine
    {
        [TextArea(2, 5)] // Permet d'afficher une zone de texte multi-lignes dans l'�diteur
        public string lineText; // Le texte brut de la ligne de dialogue

        public bool isAQuestion; // Bool�en pour savoir si cette ligne est une question

        public bool isLastDialogueLine; // Bool�en pour savoir si cette ligne est la derni�re ligne du dialogue

        [Tooltip("Select the speaker from the character list.")]
        public string speakerName; // Nom du personnage qui parle
    }

    [Header("Dialogue Lines DataBase")]
    public DialogueLine[] dialogueLines; // Tableau vide par d�faut, aucune valeur initialis�e
}

#if UNITY_EDITOR

[CustomEditor(typeof(DialogueScriptSO))]
public class DialogueScriptEditor : Editor
{
    private bool foldout = true; // Toggle pour afficher ou cacher les dialogues

    public override void OnInspectorGUI()
    {
        DialogueScriptSO dialogueScript = (DialogueScriptSO)target;

        // Affichage des lignes de dialogue sans �tre repli�es
        EditorGUILayout.LabelField("Dialogue Lines", EditorStyles.boldLabel);

        // Boucle sur chaque ligne de dialogue et affiche les options correspondantes
        for (int i = 0; i < dialogueScript.dialogueLines?.Length; i++)
        {
            DialogueScriptSO.DialogueLine line = dialogueScript.dialogueLines[i];

            // Affichage de l'index de la ligne de dialogue
            EditorGUILayout.LabelField($"Line {i + 1}", EditorStyles.boldLabel);

            // Champ de texte pour la ligne de dialogue
            line.lineText = EditorGUILayout.TextArea(line.lineText, GUILayout.MinHeight(30));

            // Checkbox pour savoir si cette ligne est une question
            line.isAQuestion = EditorGUILayout.Toggle("Is a Question?", line.isAQuestion);

            // Ajout du nouveau bool�en isLastDialogueLine
            line.isLastDialogueLine = EditorGUILayout.Toggle("Is Last Dialogue Line?", line.isLastDialogueLine);

            // Menu d�roulant pour s�lectionner le personnage
            int selectedIndex = System.Array.IndexOf(dialogueScript.characterNames, line.speakerName);
            selectedIndex = EditorGUILayout.Popup("Speaker", selectedIndex, dialogueScript.characterNames);

            // Met � jour le nom du personnage si un �l�ment est s�lectionn�
            if (selectedIndex >= 0 && selectedIndex < dialogueScript.characterNames.Length)
            {
                line.speakerName = dialogueScript.characterNames[selectedIndex];
            }
        }

        // Section repli�e pour la gestion des lignes de dialogue
        foldout = EditorGUILayout.Foldout(foldout, "Manage Dialogue Lines");
        if (foldout)
        {
            // Bouton pour ajouter une nouvelle ligne de dialogue
            if (GUILayout.Button("Add New Dialogue Line"))
            {
                ArrayUtility.Add(ref dialogueScript.dialogueLines, new DialogueScriptSO.DialogueLine());
                EditorUtility.SetDirty(dialogueScript); // Sauvegarde les changements
            }

            // Bouton pour supprimer la derni�re ligne de dialogue
            if (dialogueScript.dialogueLines?.Length > 0 && GUILayout.Button("Remove Last Dialogue Line"))
            {
                ArrayUtility.RemoveAt(ref dialogueScript.dialogueLines, dialogueScript.dialogueLines.Length - 1);
                EditorUtility.SetDirty(dialogueScript); // Sauvegarde les changements
            }
        }
        // Affiche l'inspecteur par d�faut pour les champs autres que les dialogues
        DrawDefaultInspector();
    }
}

#endif