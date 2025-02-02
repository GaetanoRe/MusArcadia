using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MaterialFixer))]
public class MaterialFixerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Récupérer l'objet cible
        MaterialFixer fixer = (MaterialFixer)target;

        // Créer un menu déroulant pour choisir le shader de base
        selectedBaseShaderIndex = EditorGUILayout.Popup("Shader de base", selectedBaseShaderIndex, shaderOptions);

        // Créer un menu déroulant pour choisir le shader à appliquer
        selectedApplyShaderIndex = EditorGUILayout.Popup("Shader à appliquer", selectedApplyShaderIndex, shaderOptions);

        // Affecter les shaders en fonction de la sélection
        if (selectedBaseShaderIndex == 0)
        {
            fixer.baseShader = Shader.Find("Standard");
        }
        else if (selectedBaseShaderIndex == 1)
        {
            fixer.baseShader = Shader.Find("Universal Render Pipeline/Lit");
        }

        if (selectedApplyShaderIndex == 0)
        {
            fixer.applyShader = Shader.Find("Standard");
        }
        else if (selectedApplyShaderIndex == 1)
        {
            fixer.applyShader = Shader.Find("Universal Render Pipeline/Lit");
        }

        // Vérifier si les shaders sont valides
        if (fixer.baseShader == null || fixer.applyShader == null)
        {
            EditorGUILayout.HelpBox("Un ou plusieurs shaders sont introuvables. Assurez-vous qu'ils sont correctement chargés dans le projet.", MessageType.Error);
        }

        // Ajouter un bouton pour appliquer la correction
        if (GUILayout.Button("Fixer les matériaux de tous les enfants"))
        {
            // Vérifier si les shaders sont valides avant de lancer la correction
            if (fixer.baseShader != null && fixer.applyShader != null)
            {
                // Appeler la fonction de correction pour tous les enfants (récursivement)
                fixer.FixMaterialsInAllChildren();
            }
            else
            {
                Debug.LogError("Un ou plusieurs shaders sont introuvables.");
            }
        }

        // Dessiner l'inspecteur de base (si tu veux afficher d'autres propriétés de l'objet)
        DrawDefaultInspector();
    }

    // Liste des shaders disponibles
    private string[] shaderOptions = new string[] { "Standard", "Universal Render Pipeline/Lit" };

    private int selectedBaseShaderIndex = 0;
    private int selectedApplyShaderIndex = 0;
}