using UnityEngine;

public class MaterialFixer : MonoBehaviour
{
    // Shader de base � v�rifier dans les mat�riaux
    public Shader baseShader;

    // Shader � appliquer lorsqu'un mat�riau n�cessite une correction
    public Shader applyShader;

    // Fonction pour corriger tous les mat�riaux des enfants (r�cursivement)
    public void FixMaterialsInAllChildren()
    {
        // Appeler la fonction r�cursive pour parcourir tous les enfants
        FixMaterialsInChildrenRecursively(transform);
    }

    // Fonction r�cursive pour parcourir tous les enfants (y compris les enfants d'enfants)
    private void FixMaterialsInChildrenRecursively(Transform parent)
    {
        // V�rifier chaque enfant
        foreach (Transform child in parent)
        {
            // V�rifier si l'objet enfant a un Renderer
            Renderer renderer = child.GetComponent<Renderer>();
            if (renderer != null)
            {
                Material[] materials = renderer.sharedMaterials; // Utiliser sharedMaterials pour plusieurs mat�riaux

                // V�rifier et corriger chaque mat�riau
                foreach (Material mat in materials)
                {
                    if (mat != null && mat.shader == baseShader)
                    {
                        // Appliquer le shader s�lectionn�
                        mat.shader = applyShader;

                        // R�initialiser les propri�t�s du mat�riau
                        mat.SetColor("_Color", Color.white);
                        mat.SetFloat("_Glossiness", 0.5f); // Valeur standard pour la brillance
                        mat.SetFloat("_Metallic", 0f); // Valeur standard pour le m�tallique

                        // Appliquer le mat�riau corrig�
                        Debug.Log($"Mat�riau corrig� pour l'objet {child.name} !");
                    }
                }
            }

            // Appel r�cursif pour v�rifier les enfants de cet enfant
            if (child.childCount > 0)
            {
                FixMaterialsInChildrenRecursively(child);
            }
        }
    }
}