using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This script makes the agent follow the main character using the NavMesh system, avoid other agents with the tag "FollowerAI",
/// and also avoid the player if the player comes too close.
/// </summary>
public class FollowerCtrl : MonoBehaviour
{
    private NavMeshAgent m_Agent; // Reference to the NavMeshAgent component
    private Transform m_Target; // Reference to the target (player)
    private float m_AvoidanceRadius = 5f; // Radius within which to check for other agents to avoid
    private float m_PlayerAvoidanceRadius = 5f; // Radius within which to check if the player is too close and avoid

    // Start is called before the first frame update
    private void Start()
    {
        // Get the NavMeshAgent component attached to this GameObject
        m_Agent = GetComponent<NavMeshAgent>();

        // Set the target to the main character (PlayerCtrl singleton)
        if (PlayerCtrl.instance != null)
        {
            m_Target = PlayerCtrl.instance.transform;
        }
        else
        {
            Debug.LogWarning("PlayerCtrl instance is not found.");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_Target != null)
        {
            // Move towards the player if the distance to the player is greater than the stopping distance
            if (Vector3.Distance(this.transform.position, m_Target.position) > m_Agent.stoppingDistance)
            {
                m_Agent.enabled = true;
                m_Agent.SetDestination(m_Target.position);
            }

            // Check for other agents tagged as "FollowerAI" within a certain radius
            AvoidOtherAgents();

            // Check if the player is too close and if so, move the agent away from the player
            AvoidPlayer();
        }
    }

    /// <summary>
    /// Checks for other agents tagged as "FollowerAI" and adjusts the agent's position to avoid them.
    /// </summary>
    private void AvoidOtherAgents()
    {
        // Find all colliders in the avoidance radius
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, m_AvoidanceRadius);

        foreach (Collider col in nearbyColliders)
        {
            if (col.CompareTag("FollowerAI"))
            {
                // If another agent is within the avoidance radius, calculate the direction to move away from it
                Vector3 directionAway = transform.position - col.transform.position;
                float distanceToAgent = directionAway.magnitude;

                // If the distance is less than the stopping distance, move away
                if (distanceToAgent < m_Agent.stoppingDistance)
                {
                    // Move the agent to a position that avoids the nearby agent
                    Vector3 newDestination = transform.position + directionAway.normalized * (m_Agent.stoppingDistance - distanceToAgent);
                    m_Agent.SetDestination(newDestination);
                }
            }
        }
    }

    /// <summary>
    /// Avoids the player if the player gets too close.
    /// </summary>
    private void AvoidPlayer()
    {
        // Check the distance between the agent and the player
        float distanceToPlayer = Vector3.Distance(transform.position, m_Target.position);

        if (distanceToPlayer < m_PlayerAvoidanceRadius)
        {
            // Calculate the direction to move away from the player
            Vector3 directionAway = transform.position - m_Target.position;
            float distanceToAvoid = m_PlayerAvoidanceRadius - distanceToPlayer; // How much the agent should move away

            // Move the agent away from the player
            Vector3 newDestination = transform.position + directionAway.normalized * distanceToAvoid;

            // Instead of disabling the agent, just give it a new destination to avoid the player
            m_Agent.SetDestination(newDestination);
        }
    }
}