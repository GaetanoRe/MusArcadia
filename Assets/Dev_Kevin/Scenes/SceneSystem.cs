using System.Collections.Generic;
using UnityEditor; // Required for using SceneAsset
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR

/// <summary>
/// Represents a reference to a scene that can be assigned in the Inspector.
/// Used to store scene information for loading/unloading purposes.
/// </summary>
[System.Serializable]
public class SceneReference
{
    [SerializeField] private SceneAsset sceneAsset; // Allows you to drag and drop a scene in the inspector

    public string SceneName
    {
        get
        {
            // Returns the name of the scene only if a scene is assigned
            return sceneAsset != null ? sceneAsset.name : string.Empty;
        }
    }
}

#endif

/// <summary>
/// Manages the loading and unloading of scenes at runtime.
/// This system is used to load scenes that are specified in the inspector at the start of the game.
/// </summary>
public class SceneSystem : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// List of scenes to be loaded at the start of the game.
    /// These scenes are added to the scene manager in additive mode.
    /// </summary>
    [Header("Develop Scenes At Start")]
    [Space(10)]
    [SerializeField] private List<SceneReference> scenesToLoadAtStart;

    #endregion Fields

    #region Unity Methods

    /// <summary>
    /// Called at the start of the game.
    /// This method triggers the loading of scenes specified in the scenesToLoadAtStart list.
    /// </summary>
    private void Start()
    {
        // Call method to load scenes
        LoadScenesAtStart();
    }

    #endregion Unity Methods

    #region Custom Methods

    /// <summary>
    /// Loads all scenes specified in the scenesToLoad list.
    /// If a scene is already loaded, it will be unloaded before being reloaded.
    /// </summary>
    private void LoadScenesAtStart()
    {
        // Iterate through the list of scenes
        foreach (SceneReference sceneRef in scenesToLoadAtStart)
        {
            string sceneName = sceneRef.SceneName; // Gets the scene name from the SceneReference
            if (string.IsNullOrEmpty(sceneName))
                continue;

            if (IsSceneLoaded(sceneName))
            {
                Debug.Log($"Scene '{sceneName}' is loaded. Unloading...");
                UnloadScene(sceneName);
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
                Debug.Log($"Scene '{sceneName}' loaded.");
            }
            else
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
                Debug.Log($"Scene '{sceneName}' loaded.");
            }
        }
    }

    /// <summary>
    /// Checks if a scene is already loaded in the scene manager.
    /// </summary>
    /// <param name="name">Name of the scene to check</param>
    /// <returns>True if the scene is loaded, False otherwise</returns>
    private bool IsSceneLoaded(string name)
    {
        // Loops through all currently loaded scenes
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == name)
            {
                return true; // The scene is already loaded
            }
        }
        return false; // The scene is not loaded
    }

    /// <summary>
    /// Unloads a scene by name if it is currently loaded.
    /// </summary>
    /// <param name="name">Name of the scene to unload</param>
    private void UnloadScene(string name)
    {
        // Checks if the scene is currently active
        Scene scene = SceneManager.GetSceneByName(name);

        if (scene.IsValid() && scene.isLoaded)
        {
            // Deactivates the objects in the scene before unloading it
            SceneManager.UnloadSceneAsync(name);
        }
    }

    #endregion Custom Methods
}