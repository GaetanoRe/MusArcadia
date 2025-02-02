using UnityEngine;

public class MaterialFixer : MonoBehaviour
{
    // Shader de base à vérifier dans les matériaux
    public Shader baseShader;

    // Shader à appliquer lorsqu'un matériau nécessite une correction
    public Shader applyShader;

    // Fonction pour corriger tous les matériaux des enfants (récursivement)
    public void FixMaterialsInAllChildren()
    {
        // Appeler la fonction récursive pour parcourir tous les enfants
        FixMaterialsInChildrenRecursively(transform);
    }

    // Fonction récursive pour parcourir tous les enfants (y compris les enfants d'enfants)
    private void FixMaterialsInChildrenRecursively(Transform parent)
    {
        // Vérifier chaque enfant
        foreach (Transform child in parent)
        {
            // Vérifier si l'objet enfant a un Renderer
            Renderer renderer = child.GetComponent<Renderer>();
            if (renderer != null)
            {
                Material[] materials = renderer.sharedMaterials; // Utiliser sharedMaterials pour plusieurs matériaux

                // Vérifier et corriger chaque matériau
                foreach (Material mat in materials)
                {
                    if (mat != null && mat.shader == baseShader)
                    {
                        // Appliquer le shader sélectionné
                        mat.shader = applyShader;

                        // Réinitialiser les propriétés du matériau
                        mat.SetColor("_Color", Color.white);
                        mat.SetFloat("_Glossiness", 0.5f); // Valeur standard pour la brillance
                        mat.SetFloat("_Metallic", 0f); // Valeur standard pour le métallique

                        // Appliquer le matériau corrigé
                        Debug.Log($"Matériau corrigé pour l'objet {child.name} !");
                    }
                }
            }

            // Appel récursif pour vérifier les enfants de cet enfant
            if (child.childCount > 0)
            {
                FixMaterialsInChildrenRecursively(child);
            }
        }
    }
}