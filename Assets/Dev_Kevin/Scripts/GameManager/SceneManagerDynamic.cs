using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerDynamic : MonoBehaviour
{
    [SerializeField] private GameStateSO gameStateSO; // Référence au ScriptableObject GameStateSO
    private List<string> loadedScenes = new List<string>(); // Liste des scènes actuellement chargées

    private void Start()
    {
        // Charge initialement les scènes du GameState actuel
        LoadScenesForCurrentState();
    }

    private void Update()
    {
        // Vérifie régulièrement l'état du jeu et charge les scènes appropriées
        LoadScenesForCurrentState();
    }

    /// <summary>
    /// Appelé pour charger dynamiquement les scènes en fonction du GameState actuel.
    /// Décharge les scènes précédemment chargées avant de charger les nouvelles.
    /// </summary>
    private void LoadScenesForCurrentState()
    {
        // Récupère les scènes associées à l'état actuel
        List<string> scenesToLoad = gameStateSO.GetScenesForState(gameStateSO.currentState);

        // Décharge les scènes précédemment chargées
        foreach (string scene in loadedScenes)
        {
            if (SceneManager.GetSceneByName(scene).isLoaded)
            {
                Debug.Log($"Unloading scene: {scene}");
                SceneManager.UnloadSceneAsync(scene);
            }
        }

        loadedScenes.Clear();

        // Charge les nouvelles scènes
        foreach (string sceneName in scenesToLoad)
        {
            if (!string.IsNullOrEmpty(sceneName) && !SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                Debug.Log($"Loading scene: {sceneName}");
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                loadedScenes.Add(sceneName); // Ajouter à la liste des scènes chargées
            }
            else
            {
                Debug.LogWarning($"Scene name is empty or already loaded: {sceneName}");
            }
        }
    }
}