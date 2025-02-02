using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MaterialFixer))]
public class MaterialFixerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // R�cup�rer l'objet cible
        MaterialFixer fixer = (MaterialFixer)target;

        // Cr�er un menu d�roulant pour choisir le shader de base
        selectedBaseShaderIndex = EditorGUILayout.Popup("Shader de base", selectedBaseShaderIndex, shaderOptions);

        // Cr�er un menu d�roulant pour choisir le shader � appliquer
        selectedApplyShaderIndex = EditorGUILayout.Popup("Shader � appliquer", selectedApplyShaderIndex, shaderOptions);

        // Affecter les shaders en fonction de la s�lection
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

        // V�rifier si les shaders sont valides
        if (fixer.baseShader == null || fixer.applyShader == null)
        {
            EditorGUILayout.HelpBox("Un ou plusieurs shaders sont introuvables. Assurez-vous qu'ils sont correctement charg�s dans le projet.", MessageType.Error);
        }

        // Ajouter un bouton pour appliquer la correction
        if (GUILayout.Button("Fixer les mat�riaux de tous les enfants"))
        {
            // V�rifier si les shaders sont valides avant de lancer la correction
            if (fixer.baseShader != null && fixer.applyShader != null)
            {
                // Appeler la fonction de correction pour tous les enfants (r�cursivement)
                fixer.FixMaterialsInAllChildren();
            }
            else
            {
                Debug.LogError("Un ou plusieurs shaders sont introuvables.");
            }
        }

        // Dessiner l'inspecteur de base (si tu veux afficher d'autres propri�t�s de l'objet)
        DrawDefaultInspector();
    }

    // Liste des shaders disponibles
    private string[] shaderOptions = new string[] { "Standard", "Universal Render Pipeline/Lit" };

    private int selectedBaseShaderIndex = 0;
    private int selectedApplyShaderIndex = 0;
}