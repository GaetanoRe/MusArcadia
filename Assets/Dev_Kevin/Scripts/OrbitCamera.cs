using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target; // The GameObject the camera will orbit around
    public float distance = 25.0f; // Distance between the camera and the GameObject
    public float rotationSpeed = 100.0f; // Rotation speed
    public Vector2 pitchLimits = new Vector2(-30, 60); // Vertical rotation (pitch) limits
    public Vector2 zoomLimits = new Vector2(20.0f, 40.0f); // Min and max zoom distance
    public float zoomSpeed = 10.0f; // Speed of zooming with the mouse wheel
    public float minYPosition = 2.0f; // Minimum allowed y position for the camera

    private float yaw = 0.0f; // Horizontal rotation
    private float pitch = 0.0f; // Vertical rotation

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("The 'target' field is not assigned. Please assign it in the Inspector!");
            return;
        }

        // Initialize angles to match the current camera rotation
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // Get mouse input for rotation
        float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

        // Get vertical input
        float verticalInput = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // Calculate new pitch value
        float newPitch = pitch + verticalInput;

        // Check if the camera would go below the minimum height
        Quaternion testRotation = Quaternion.Euler(newPitch, yaw, 0);
        Vector3 testOffset = testRotation * new Vector3(0, 0, -distance);
        float testHeight = target.position.y + testOffset.y;

        // Allow vertical input only if the test height is above the minimum or the input is moving upward
        if (testHeight >= minYPosition || verticalInput > 0)
        {
            pitch = newPitch;
        }
        else
        {
            Debug.Log("La caméra ne peut pas descendre plus bas que la hauteur minimale !");
        }

        // Apply horizontal input
        yaw += horizontalInput;

        // Apply vertical rotation limits
        pitch = Mathf.Clamp(pitch, pitchLimits.x, pitchLimits.y);

        // Adjust distance using the mouse wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        distance -= scrollInput;
        distance = Mathf.Clamp(distance, zoomLimits.x, zoomLimits.y); // Apply zoom limits

        // Calculate the final camera position
        Quaternion finalRotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 offset = finalRotation * new Vector3(0, 0, -distance);

        // Position the camera around the target
        transform.position = target.position + offset;
        transform.LookAt(target); // Ensure the camera always looks at the target
    }
}