using UnityEngine;

/// <summary>
/// Ensures the GameObject always faces the camera. Can optionally lock rotation to only the Y-axis.
/// </summary>
public class SpriteBillboard : MonoBehaviour
{
    /// <summary>
    /// If true, the GameObject's rotation will only follow the Y-axis to prevent tilting.
    /// </summary>
    [SerializeField] private bool freezeXZAxis = true;

    /// <summary>
    /// Called once per frame, after all Update calls. Rotates the GameObject to face the camera.
    /// </summary>
    private void LateUpdate()
    {
        if (freezeXZAxis)
        {
            // Rotate the object to match the camera's Y-axis rotation
            transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
        }
        else
        {
            // Fully match the camera's rotation
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}