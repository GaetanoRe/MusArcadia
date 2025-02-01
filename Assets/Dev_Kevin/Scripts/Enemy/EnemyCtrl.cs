using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    #region Singleton

    // Property to access the unique instance of EnemyCtrl
    public static EnemyCtrl Instance
    {
        get
        {
            // Check if the instance already exists
            if (_instance == null)
            {
                _instance = FindObjectOfType<EnemyCtrl>(); // Try to find an existing instance
                if (_instance == null)
                {
                    // If no instance is found, create a new GameObject with this script
                    GameObject singleton = new GameObject("EnemyCtrl");
                    _instance = singleton.AddComponent<EnemyCtrl>();
                }
            }
            return _instance;
        }
    }

    // Static instance for the Singleton pattern
    private static EnemyCtrl _instance;

    #endregion Singleton

    #region Initialization

    private SpriteBillboard m_SpriteBillboard; // Variable to hold the SpriteBillboard component

    [SerializeField] private bool lookAtPlayer = false; // Boolean to control if the enemy should look at the player
    private Transform playerTransform; // Variable to hold the player's transform
    private bool playerInitialized = false; // Flag to check if playerTransform is initialized

    private void Awake()
    {
        // Check if there's already an instance of this script
        if (_instance != null && _instance != this)
        {
            // If an instance exists and it's not this one, destroy this object
            Destroy(gameObject);
        }
        else
        {
            // Otherwise, set this instance as the unique instance
            _instance = this;
            // Prevent the object from being destroyed when changing scenes
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // Try to find the "GFX" child GameObject and get its SpriteBillboard component
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
        // Check if playerTransform is not initialized yet
        if (!playerInitialized)
        {
            // Try to find the "MainCharacter" GameObject and get its transform
            GameObject mainCharacter = GameObject.Find("MainCharacter");
            if (mainCharacter != null)
            {
                playerTransform = mainCharacter.transform; // Get the player's transform
                playerInitialized = true; // Mark as initialized
            }
            else
            {
                Debug.LogWarning("MainCharacter not found in the scene.");
            }
        }

        // If lookAtPlayer is true and we have a valid playerTransform, rotate towards the player
        if (lookAtPlayer && playerTransform != null)
        {
            Vector3 direction = playerTransform.position - transform.position;
            direction.y = 0; // Keep the rotation on the horizontal plane
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f); // Smooth rotation
        }
    }

    #endregion Update
}