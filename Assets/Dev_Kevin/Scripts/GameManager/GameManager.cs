using UnityEngine;

/// <summary>
/// A manager class that sets the initial game state and provides global access to the GameManager.
/// It relies on the GameStateSO ScriptableObject to manage the game's current state.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Game State Settings")]
    public GameStateSO gameStateSO;

    // Singleton Instance
    public static GameManager Instance { get; private set; }

    /// <summary>
    /// Updates the value of isEpressed in the GameStateSO.
    /// </summary>
    /// <param name="_arg">The value to set isEpressed to.</param>
    public void SetIsEpressed(bool _arg)
    {
        if (gameStateSO != null)
        {
            gameStateSO.isEpressed = _arg;
        }
        else
        {
            Debug.LogWarning("GameStateSO is not assigned in the inspector!");
        }
    }

    /// <summary>
    /// Checks the current value of isEpressed.
    /// </summary>
    public bool GetIsEpressed()
    {
        return gameStateSO != null ? gameStateSO.isEpressed : false;
    }

    private void Awake()
    {
        // Assure l'unicité du Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Supprime les doublons de GameManager
            return;
        }

        Instance = this; // Définit l'instance unique
        DontDestroyOnLoad(gameObject); // Conserve ce GameManager entre les scènes
    }

    /// <summary>
    /// Sets the initial game state to Exploration when the game starts.
    /// </summary>
    private void Start()
    {
        if (gameStateSO != null)
        {
            gameStateSO.currentState = GameStateSO.GameState.Exploration;
            gameStateSO.isEpressed = false;

            Debug.Log("Game state initialized to: " + gameStateSO.currentState);
        }
        else
        {
            Debug.LogWarning("GameStateSO is not assigned in the inspector!");
        }
    }
}