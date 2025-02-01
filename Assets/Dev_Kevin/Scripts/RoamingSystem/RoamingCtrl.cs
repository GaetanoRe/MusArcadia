using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement; // For working with scenes

public class RoamingCtrl : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private float m_Radius = 10f; // Radius for selecting a random point around the agent
    [SerializeField] private float m_InvokeInterval = 3f; // Time interval between each movement
    [SerializeField] private GameStateSO m_GameStateSO; // Reference to the ScriptableObject containing the game's state

    #endregion Serialized Fields

    #region Private Fields

    private NavMeshAgent m_Agent;
    private Vector3 m_SpawnPosition;
    private bool m_CanSetPositionInExploration;
    private GameObject m_OnBattleSpawnLocationObject;

    #endregion Private Fields

    #region Unity Lifecycle Methods

    private void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component attached to this GameObject
        m_SpawnPosition = transform.position; // Save the initial spawn position

        InvokeRepeating(nameof(MoveToRandomPoint), 0f, m_InvokeInterval); // Start moving to random points

        m_CanSetPositionInExploration = false; // Initial state where position cannot be set during exploration
    }

    private void Update()
    {
        HandleGameStates(); // Handle state transitions and behaviors during each frame
    }

    #endregion Unity Lifecycle Methods

    #region Game State Handling

    // Handle the logic based on the current game state
    private void HandleGameStates()
    {
        if (m_GameStateSO.currentState == GameStateSO.GameState.Battle)
        {
            HandleBattleState();
        }
        else if (m_GameStateSO.currentState == GameStateSO.GameState.Exploration && m_CanSetPositionInExploration)
        {
            HandleExplorationState();
        }
    }

    private void HandleBattleState()
    {
        m_Agent.enabled = false; // Disable the agent during Battle state
        m_CanSetPositionInExploration = true; // Allow position change in exploration

        try
        {
            m_OnBattleSpawnLocationObject = FindObjectInAllScenes("EnemyBattleSpawnPosition");
            if (m_OnBattleSpawnLocationObject != null)
            {
                transform.position = m_OnBattleSpawnLocationObject.transform.position; // Set position to battle spawn
            }
        }
        catch (System.Exception)
        {
            Debug.Log("Transform not found");
        }
    }

    private void HandleExplorationState()
    {
        m_CanSetPositionInExploration = false;
        transform.position = m_SpawnPosition; // Reset position to original spawn position
        m_Agent.enabled = true; // Re-enable the agent for exploration
    }

    #endregion Game State Handling

    #region Movement Logic

    // Method to move the agent to a random point within the defined radius
    private void MoveToRandomPoint()
    {
        if (m_Agent.enabled)
        {
            Vector3 randomPoint = GetRandomNavMeshPoint(transform.position, m_Radius);
            if (randomPoint != Vector3.zero)
            {
                m_Agent.SetDestination(randomPoint); // Set the destination for the agent
            }
        }
    }

    // Helper method to get a random valid point on the NavMesh
    private Vector3 GetRandomNavMeshPoint(Vector3 center, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += center;

        // Sample a valid position on the NavMesh within the radius
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, NavMesh.AllAreas))
        {
            return hit.position; // Return the valid point
        }

        return Vector3.zero; // Return zero if no valid point is found
    }

    #endregion Movement Logic

    #region Scene Handling

    // Helper method to search for an object in all loaded scenes
    private GameObject FindObjectInAllScenes(string objectName)
    {
        // Iterate through all loaded scenes
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.isLoaded)
            {
                // Search for the object in the scene
                GameObject foundObject = GameObject.Find(objectName);
                if (foundObject != null)
                {
                    return foundObject; // Return the object if found
                }
            }
        }
        return null; // Return null if not found in any scene
    }

    #endregion Scene Handling
}