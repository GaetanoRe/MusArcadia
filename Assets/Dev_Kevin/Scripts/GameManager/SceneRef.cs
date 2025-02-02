using UnityEngine;
using UnityEditor;

[System.Serializable]
public class SceneRef
{
    public string SceneName => sceneName;

    // Appel� dans l'�diteur pour synchroniser le nom de la sc�ne
    public void UpdateSceneName()
    {
        if (sceneAsset != null)
        {
            sceneName = sceneAsset.name; // Met � jour le nom de la sc�ne avec le nom de l'asset
        }
        else
        {
            sceneName = string.Empty; // Vide si aucun asset n'est s�lectionn�
        }
    }

    [SerializeField] private SceneAsset sceneAsset; // R�f�rence � une sc�ne dans l'�diteur
    [SerializeField, HideInInspector] private string sceneName; // Nom de la sc�ne pour l'ex�cution
}