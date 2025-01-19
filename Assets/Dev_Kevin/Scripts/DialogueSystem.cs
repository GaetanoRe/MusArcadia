using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance; // Singleton reference

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