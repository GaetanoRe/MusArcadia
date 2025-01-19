using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game State Settings")]
    [SerializeField] private GameStateSO gameStateSO;  // R�f�rence au ScriptableObject GameStateSO

    private void Start()
    {
        if (gameStateSO != null)
        {
            // D�finit l'�tat actuel � Exploration au d�marrage
            gameStateSO.currentState = GameStateSO.GameState.Exploration;
            Debug.Log("Game state initialis� � : " + gameStateSO.currentState);
        }
        else
        {
            Debug.LogWarning("Le GameStateSO n'est pas assign� dans l'inspecteur !");
        }
    }
}