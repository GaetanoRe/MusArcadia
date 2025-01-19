using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game State Settings")]
    [SerializeField] private GameStateSO gameStateSO;  // Référence au ScriptableObject GameStateSO

    private void Start()
    {
        if (gameStateSO != null)
        {
            // Définit l'état actuel à Exploration au démarrage
            gameStateSO.currentState = GameStateSO.GameState.Exploration;
            Debug.Log("Game state initialisé à : " + gameStateSO.currentState);
        }
        else
        {
            Debug.LogWarning("Le GameStateSO n'est pas assigné dans l'inspecteur !");
        }
    }
}