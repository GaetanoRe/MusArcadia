using UnityEngine;

/// <summary>
/// This script manages player movement control and ensures a single instance of the player controller
/// persists across scenes. It also synchronizes the movement state with a PlayerSettingsSO object.
/// </summary>
public class PlayerCtrl : MonoBehaviour
{
    #region Singleton

    // Singleton instance to ensure only one PlayerCtrl exists
    public static PlayerCtrl instance;

    private void Awake()
    {
        // Implements the singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps this object alive across scenes
        }
        else
        {
            Destroy(gameObject); // Destroys duplicates to maintain singleton
        }
    }

    #endregion Singleton

    #region Variables

    // Indicates if the player is allowed to move
    public bool canPlayerMove;

    // Reference to a ScriptableObject that stores player settings
    [SerializeField] private PlayerSettingsSO playerSettingsSO;

    #endregion Variables

    #region Unity Methods

    private void Start()
    {
        // Player movement is enabled by default
        canPlayerMove = true;
    }

    private void Update()
    {
        // Synchronizes the movement state with the ScriptableObject
        playerSettingsSO.canPlayerMove = canPlayerMove;
    }

    #endregion Unity Methods

    #region Public Methods

    /// <summary>
    /// Enables or disables player movement.
    /// </summary>
    /// <param name="_arg">True to allow movement, false to restrict it.</param>
    public void SetEnablePlayerMovement(bool _arg)
    {
        canPlayerMove = _arg;
    }

    #endregion Public Methods
}