using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target; // The GameObject the camera will orbit around
    public float distance = 25.0f; // Distance between the camera and the GameObject
    public float rotationSpeed = 100.0f; // Rotation speed
    public Vector2 pitchLimits = new Vector2(-30, 60); // Vertical rotation (pitch) limits
    public Vector2 zoomLimits = new Vector2(20.0f, 40.0f); // Min and max zoom distance
    public float zoomSpeed = 10.0f; // Speed of zooming with the mouse wheel

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
        float verticalInput = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // Calculate new rotations
        yaw += horizontalInput;
        pitch += verticalInput;

        // Apply vertical rotation limits
        pitch = Mathf.Clamp(pitch, pitchLimits.x, pitchLimits.y);

        // Adjust distance using the mouse wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        distance -= scrollInput;
        distance = Mathf.Clamp(distance, zoomLimits.x, zoomLimits.y); // Apply zoom limits

        // Calculate the new camera position
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        // Position the camera around the GameObject
        transform.position = target.position + offset;
        transform.LookAt(target); // Ensure the camera always looks at the target
    }
}