using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStateSO", menuName = "Game/StateSO")]
public class GameStateSO : ScriptableObject
{
    // The current game state (Menu, Battle, or Exploration)
    public GameState currentState;

    [Header("Battle State Settings")]
    public List<SceneRef> battleScenes; // List of scenes for the Battle state

    [Header("Menu State Settings")]
    public List<SceneRef> menuScenes; // List of scenes for the Menu state

    [Header("Exploration State Settings")]
    public List<SceneRef> explorationScenes; // List of scenes for the Exploration state

    /// <summary>
    /// Enumeration representing the different game states: Menu, Battle, and Exploration.
    /// </summary>
    public enum GameState
    {
        Menu,
        Battle,
        Exploration,
    }

    /// <summary>
    /// Gets the list of scene names associated with the given game state.
    /// </summary>
    /// <param name="state">The game state for which to retrieve the scene names.</param>
    /// <returns>A list of scene names for the specified state.</returns>
    public List<string> GetScenesForState(GameState state)
    {
        List<string> sceneNames = new List<string>();

        switch (state)
        {
            case GameState.Battle:
                foreach (var sceneRef in battleScenes)
                {
                    sceneRef.UpdateSceneName(); // Update the scene name
                    sceneNames.Add(sceneRef.SceneName);
                }
                break;

            case GameState.Menu:
                foreach (var sceneRef in menuScenes)
                {
                    sceneRef.UpdateSceneName(); // Update the scene name
                    sceneNames.Add(sceneRef.SceneName);
                }
                break;

            case GameState.Exploration:
                foreach (var sceneRef in explorationScenes)
                {
                    sceneRef.UpdateSceneName(); // Update the scene name
                    sceneNames.Add(sceneRef.SceneName);
                }
                break;
        }

        return sceneNames;
    }
}