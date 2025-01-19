using UnityEngine;

/// <summary>
/// A manager class that sets the initial game state to Exploration at the start of the game.
/// It relies on the GameStateSO ScriptableObject to manage the game's current state.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Game State Settings")]
    [SerializeField] private GameStateSO gameStateSO;

    // Reference to the ScriptableObject that holds the game's state.

    /// <summary>
    /// Called when the game starts. Sets the initial game state to Exploration.
    /// </summary>
    private void Start()
    {
        if (gameStateSO != null)
        {
            // Set the current game state to Exploration at the start of the game.
            gameStateSO.currentState = GameStateSO.GameState.Exploration;
            Debug.Log("Game state initialized to: " + gameStateSO.currentState);
        }
        else
        {
            // Log a warning if the GameStateSO reference is not assigned in the inspector.
            Debug.LogWarning("GameStateSO is not assigned in the inspector!");
        }
    }
}