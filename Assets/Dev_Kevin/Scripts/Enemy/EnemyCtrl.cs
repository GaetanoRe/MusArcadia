using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    #region Singleton

    // Property to access the unique instance of EnemyCtrl
    public static EnemyCtrl Instance { get; private set; }

    // Static instance for the Singleton pattern
    private static bool _isInstanceCreated = false;

    #endregion Singleton

    #region Initialization

    private SpriteBillboard m_SpriteBillboard; // Variable to hold the SpriteBillboard component

    [SerializeField] private bool m_LookAtPlayer = false; // Boolean to control if the enemy should look at the player
    private Transform m_PlayerTransform; // Variable to hold the player's transform
    private bool m_PlayerInitialized = false; // Flag to check if playerTransform is initialized

    // Ensure the object is initialized properly and prevents duplication
    private void Awake()
    {
        // If an instance already exists, destroy this object
        if (_isInstanceCreated && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set the instance if it's not already set
        if (!Instance)
        {
            Instance = this;
            _isInstanceCreated = true;
            DontDestroyOnLoad(gameObject); // Prevent destruction on scene load
        }
    }

    // Try to get the SpriteBillboard component during the start phase
    private void Start()
    {
        GameObject gfx = transform.Find("GFX")?.gameObject; // Find the GFX child
        if (gfx != null)
        {
            m_SpriteBillboard = gfx.GetComponent<SpriteBillboard>(); // Get the SpriteBillboard component
        }
        else
        {
            Debug.LogWarning("GFX GameObject not found as a child of " + gameObject.name);
        }
    }

    #endregion Initialization

    #region Update

    private void Update()
    {
        InitializePlayer(); // Initialize player transform if not already done
        HandleLookAtPlayer(); // Handle the enemy's rotation towards the player
    }

    // Method to initialize the player transform
    private void InitializePlayer()
    {
        // If playerTransform is not initialized yet, find the "MainCharacter" GameObject
        if (!m_PlayerInitialized)
        {
            GameObject mainCharacter = GameObject.Find("MainCharacter");
            if (mainCharacter != null)
            {
                m_PlayerTransform = mainCharacter.transform; // Get the player's transform
                m_PlayerInitialized = true; // Mark as initialized
            }
            else
            {
                Debug.LogWarning("MainCharacter not found in the scene.");
            }
        }
    }

    // Method to handle the enemy's rotation towards the player
    private void HandleLookAtPlayer()
    {
        if (m_LookAtPlayer && m_PlayerTransform != null)
        {
            Vector3 direction = m_PlayerTransform.position - transform.position;
            direction.y = 0; // Keep the rotation on the horizontal plane
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f); // Smooth rotation
        }
    }

    #endregion Update
}