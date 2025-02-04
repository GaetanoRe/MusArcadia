using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[System.Serializable]

public class SceneRef
{
    public string SceneName => sceneName;

    // Appel� dans l'�diteur pour synchroniser le nom de la sc�ne


    public void UpdateSceneName()
    {
#if UNITY_EDITOR
        if (sceneAsset != null)
        {
            sceneName = sceneAsset.name; // Met � jour le nom de la sc�ne avec le nom de l'asset
        }
        else
        {
            sceneName = string.Empty; // Vide si aucun asset n'est s�lectionn�
        }
#endif
    }

#if UNITY_EDITOR
    [SerializeField] private SceneAsset sceneAsset; // R�f�rence � une sc�ne dans l'�diteur
#endif
    [SerializeField, HideInInspector] private string sceneName; // Nom de la sc�ne pour l'ex�cution

}