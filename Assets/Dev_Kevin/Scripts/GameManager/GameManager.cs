using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void StartBattleState()
    {
        // Change l'état en "Battle"
        UpdateGameState(GameStateSO.GameState.Battle);
    }

    private static GameManager instance;

    [Header("Game State Settings")]
    [SerializeField] private GameStateSO gameStateSO;  // Référence au ScriptableObject GameStateSO

    private void Awake()
    {
        // Enregistrer l'instance pour l'utiliser dans tout le jeu
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Garder l'objet lors des changements de scène
        }
        else
        {
            Destroy(gameObject);  // Si une autre instance existe déjà, on détruit cet objet
        }
    }

    private void UpdateGameState(GameStateSO.GameState newState)
    {
        // Change l'état actuel et charge les scènes associées
        gameStateSO.currentState = newState;
        List<string> scenesToLoad = gameStateSO.GetScenesForState(newState);

        // Affiche les scènes à charger pour cet état
        foreach (var sceneName in scenesToLoad)
        {
            Debug.Log("Scene to load: " + sceneName);
            // Charger la scène en mode Additive
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }
}