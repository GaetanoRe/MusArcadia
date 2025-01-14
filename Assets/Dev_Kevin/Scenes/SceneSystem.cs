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
        //// Charge la sc�ne Dev de mani�re normale (remplace la sc�ne actuelle)
        //SceneManager.LoadScene("Dev_Kevin", LoadSceneMode.Single);

        // Charge la sc�ne Overworld de mani�re additive (ajoute la sc�ne sans la remplacer)
        SceneManager.LoadScene("Overworld", LoadSceneMode.Additive);
    }
}