using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStateSO", menuName = "Game/StateSO")]
public class GameStateSO : ScriptableObject
{
    public GameState currentState;

    [Header("Battle State Settings")]
    public List<SceneRef> battleScenes;

    [Header("Menu State Settings")]
    public List<SceneRef> menuScenes;

    [Header("Exploration State Settings")]
    public List<SceneRef> explorationScenes; // Ajout� ici

    public enum GameState
    {
        Menu,
        Battle,
        Exploration,
    }

    public List<string> GetScenesForState(GameState state)
    {
        List<string> sceneNames = new List<string>();

        switch (state)
        {
            case GameState.Battle:
                foreach (var sceneRef in battleScenes)
                {
                    sceneRef.UpdateSceneName(); // Mise � jour du nom de la sc�ne
                    sceneNames.Add(sceneRef.SceneName);
                }
                break;

            case GameState.Menu:
                foreach (var sceneRef in menuScenes)
                {
                    sceneRef.UpdateSceneName(); // Mise � jour du nom de la sc�ne
                    sceneNames.Add(sceneRef.SceneName);
                }
                break;

            case GameState.Exploration: // Ajout� ici
                foreach (var sceneRef in explorationScenes)
                {
                    sceneRef.UpdateSceneName(); // Mise � jour du nom de la sc�ne
                    sceneNames.Add(sceneRef.SceneName);
                }
                break;
        }

        return sceneNames;
    }
}