using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void StartBattleState()
    {
        // Change l'�tat en "Battle"
        UpdateGameState(GameStateSO.GameState.Battle);
    }

    private static GameManager instance;

    [Header("Game State Settings")]
    [SerializeField] private GameStateSO gameStateSO;  // R�f�rence au ScriptableObject GameStateSO

    private void Awake()
    {
        // Enregistrer l'instance pour l'utiliser dans tout le jeu
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Garder l'objet lors des changements de sc�ne
        }
        else
        {
            Destroy(gameObject);  // Si une autre instance existe d�j�, on d�truit cet objet
        }
    }

    private void UpdateGameState(GameStateSO.GameState newState)
    {
        // Change l'�tat actuel et charge les sc�nes associ�es
        gameStateSO.currentState = newState;
        List<string> scenesToLoad = gameStateSO.GetScenesForState(newState);

        // Affiche les sc�nes � charger pour cet �tat
        foreach (var sceneName in scenesToLoad)
        {
            Debug.Log("Scene to load: " + sceneName);
            // Charger la sc�ne en mode Additive
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }
}