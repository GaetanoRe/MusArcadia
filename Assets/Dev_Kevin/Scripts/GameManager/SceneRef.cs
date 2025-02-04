using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[System.Serializable]

public class SceneRef
{
    public string SceneName => sceneName;

    // Appelé dans l'éditeur pour synchroniser le nom de la scène


    public void UpdateSceneName()
    {
#if UNITY_EDITOR
        if (sceneAsset != null)
        {
            sceneName = sceneAsset.name; // Met à jour le nom de la scène avec le nom de l'asset
        }
        else
        {
            sceneName = string.Empty; // Vide si aucun asset n'est sélectionné
        }
#endif
    }

#if UNITY_EDITOR
    [SerializeField] private SceneAsset sceneAsset; // Référence à une scène dans l'éditeur
#endif
    [SerializeField, HideInInspector] private string sceneName; // Nom de la scène pour l'exécution

}