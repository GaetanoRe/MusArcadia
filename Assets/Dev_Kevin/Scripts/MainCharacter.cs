using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance; // Singleton reference

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps this object alive between scenes
        }
        else
        {
            Destroy(gameObject); // Destroys any duplicate
        }
    }
}