using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Scene System
/// </summary>
public class SceneSystem : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        //// Charge la scène Dev de manière normale (remplace la scène actuelle)
        //SceneManager.LoadScene("Dev_Kevin", LoadSceneMode.Single);

        // Charge la scène Overworld de manière additive (ajoute la scène sans la remplacer)
        SceneManager.LoadScene("Overworld", LoadSceneMode.Additive);
    }
}