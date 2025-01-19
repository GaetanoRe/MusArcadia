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
        }
    }
}