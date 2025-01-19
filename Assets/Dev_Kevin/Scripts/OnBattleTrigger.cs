using UnityEngine;

/// <summary>
/// A component that triggers a transition to the Battle game state
/// when the player enters the trigger area.
/// </summary>
public class OnBattleTrigger : MonoBehaviour
{
    [SerializeField] private GameStateSO gameStateSO; // Reference to the ScriptableObject containing the game's state.

    /// <summary>
    /// Called when a Collider enters the trigger attached to this GameObject.
    /// If the Collider belongs to the Player, the game state is switched to Battle.
    /// </summary>
    /// <param name="other">The Collider entering the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the Player (tagged as "Player").
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger. Switching to Battle State.");

            if (gameStateSO != null)
            {
                // Update the current game state to Battle.
                gameStateSO.currentState = GameStateSO.GameState.Battle;
                Debug.Log("Game state updated to: " + gameStateSO.currentState);
            }
            else
            {
                // Log a warning if the GameStateSO reference is not set in the inspector.
                Debug.LogWarning("GameStateSO is not assigned in the inspector!");
            }
        }
    }
}