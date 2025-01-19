using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerDynamic : MonoBehaviour
{
    [SerializeField] private GameStateSO gameStateSO; // R�f�rence au ScriptableObject GameStateSO
    private List<string> loadedScenes = new List<string>(); // Liste des sc�nes actuellement charg�es

    private void Start()
    {
        // Charge initialement les sc�nes du GameState actuel
        LoadScenesForCurrentState();
    }

    private void Update()
    {
        // V�rifie r�guli�rement l'�tat du jeu et charge les sc�nes appropri�es
        LoadScenesForCurrentState();
    }

    /// <summary>
    /// Appel� pour charger dynamiquement les sc�nes en fonction du GameState actuel.
    /// D�charge les sc�nes pr�c�demment charg�es avant de charger les nouvelles.
    /// </summary>
    private void LoadScenesForCurrentState()
    {
        // R�cup�re les sc�nes associ�es � l'�tat actuel
        List<string> scenesToLoad = gameStateSO.GetScenesForState(gameStateSO.currentState);

        // D�charge les sc�nes pr�c�demment charg�es
        foreach (string scene in loadedScenes)
        {
            if (SceneManager.GetSceneByName(scene).isLoaded)
            {
                Debug.Log($"Unloading scene: {scene}");
                SceneManager.UnloadSceneAsync(scene);
            }
        }

        loadedScenes.Clear();

        // Charge les nouvelles sc�nes
        foreach (string sceneName in scenesToLoad)
        {
            if (!string.IsNullOrEmpty(sceneName) && !SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                Debug.Log($"Loading scene: {sceneName}");
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                loadedScenes.Add(sceneName); // Ajouter � la liste des sc�nes charg�es
            }
            else
            {
                Debug.LogWarning($"Scene name is empty or already loaded: {sceneName}");
            }
        }
    }
}