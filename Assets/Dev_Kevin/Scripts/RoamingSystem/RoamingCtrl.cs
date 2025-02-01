using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement; // N'oublie pas d'inclure ce namespace pour travailler avec les scènes

public class RoamingCtrl : MonoBehaviour
{
    public GameObject OnBattleSpawnLocationObject;
    public Vector3 SpawnPosition;

    public bool canSetPositionInExploration;

    // Serialized fields for customizable radius and invoke interval
    [SerializeField] private float m_Radius = 10f; // Radius for selecting a random point around the agent

    [SerializeField] private float m_InvokeInterval = 3f; // Time interval between each movement

    [SerializeField] private GameStateSO gameStateSO; // Reference to the ScriptableObject containing the game's state.

    private NavMeshAgent m_Agent;

    private void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component attached to this GameObject

        SpawnPosition = GetComponent<Transform>().position;

        InvokeRepeating(nameof(MoveToRandomPoint), 0f, m_InvokeInterval);

        canSetPositionInExploration = false;
    }

    private void Update()
    {
        // Check if the game is in "Battle" state
        if (gameStateSO.currentState == GameStateSO.GameState.Battle)
        {
            // Stop the roaming behavior during the Battle state
            m_Agent.enabled = false;

            canSetPositionInExploration = true;

            try
            {
                OnBattleSpawnLocationObject = FindObjectInAllScenes("EnemyBattleSpawnPosition");

                this.transform.position = OnBattleSpawnLocationObject.transform.position;
            }
            catch (System.Exception)
            {
                Debug.Log("transform not found");
            }
        }
        if (gameStateSO.currentState == GameStateSO.GameState.Exploration && canSetPositionInExploration == true)
        {
            canSetPositionInExploration = false;
            this.transform.position = SpawnPosition;
            m_Agent.enabled = true;
        }
    }

    // Method to move the agent to a random point within the defined radius
    private void MoveToRandomPoint()
    {
        if (m_Agent.enabled == true)
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
}