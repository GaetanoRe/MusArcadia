using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerDynamic : MonoBehaviour
{
    [SerializeField] private GameStateSO gameStateSO; // Reference to the ScriptableObject holding the game state.
    private GameStateSO.GameState previousState;      // Tracks the previous game state.

    private void Start()
    {
        if (gameStateSO != null)
        {
            // Initialize the previous state with the current state from the ScriptableObject.
            previousState = gameStateSO.currentState;
        }
        else
        {
            // Log an error if the GameStateSO reference is not assigned.
            Debug.LogError("GameStateSO is not assigned in the inspector!");
        }
    }

    private void Update()
    {
        // Check if the game state has changed.
        if (gameStateSO != null && gameStateSO.currentState != previousState)
        {
            Debug.Log($"GameState changed from {previousState} to {gameStateSO.currentState}");

            // Unload scenes associated with the previous state (except the first one).
            UnloadScenesForState(previousState);

            // Load scenes associated with the new state.
            LoadScenesForState(gameStateSO.currentState);

            // Update the previous state to the current state.
            previousState = gameStateSO.currentState;
        }
    }

    /// <summary>
    /// Loads all scenes associated with the given game state.
    /// Skips scenes that are already loaded.
    /// </summary>
    /// <param name="state">The game state for which to load scenes.</param>
    private void LoadScenesForState(GameStateSO.GameState state)
    {
        List<string> scenesToLoad = gameStateSO.GetScenesForState(state);

        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            string sceneName = scenesToLoad[i];

            // Load the scene if it is not already loaded.
            if (!string.IsNullOrEmpty(sceneName) && !SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                Debug.Log($"Loading scene: {sceneName}");
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
        }
    }

    /// <summary>
    /// Unloads all scenes associated with the given game state, except the first scene (index 0).
    /// </summary>
    /// <param name="state">The game state for which to unload scenes.</param>
    private void UnloadScenesForState(GameStateSO.GameState state)
    {
        List<string> scenesToUnload = gameStateSO.GetScenesForState(state);

        // Start from index 1 to ensure the first scene is never unloaded.
        for (int i = 1; i < scenesToUnload.Count; i++)
        {
            string sceneName = scenesToUnload[i];

            // Unload the scene if it is currently loaded.
            if (SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                Debug.Log($"Unloading scene: {sceneName}");
                SceneManager.UnloadSceneAsync(sceneName);
            }
        }
    }
}