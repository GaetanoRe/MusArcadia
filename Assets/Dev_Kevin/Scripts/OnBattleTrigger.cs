using UnityEngine;

public class OnBattleTrigger : MonoBehaviour
{
    [SerializeField] private GameStateSO gameStateSO; // Référence au ScriptableObject GameStateSO

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet qui entre dans le trigger est le joueur
        if (other.CompareTag("Player")) // Assurez-vous que le joueur a le tag "Player"
        {
            Debug.Log("Player entered the trigger. Switching to Battle State.");

            if (gameStateSO != null)
            {
                // Change l'état du GameStateSO en Battle
                gameStateSO.currentState = GameStateSO.GameState.Battle;
                Debug.Log("Game state updated to: " + gameStateSO.currentState);
            }
            else
            {
                Debug.LogWarning("GameStateSO is not assigned in the inspector!");
            }
        }
    }
}