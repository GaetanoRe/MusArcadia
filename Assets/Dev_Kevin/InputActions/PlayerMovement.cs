using UnityEngine;
using UnityEngine.AI; // Don't forget to add this reference to use NavMeshAgent

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 10f; // Jump force
    public float rotationSpeed = 700f; // Rotation speed
    public Camera playerCamera; // Reference to the player's camera
    [SerializeField] private GameStateSO gameStateSO;
    [SerializeField] private PlayerSettingsSO playerSettingsSO;
    private NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent
    private Rigidbody rb; // The 3D Rigidbody of the character, necessary for jumping

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Retrieves the 3D Rigidbody attached to this GameObject
        navMeshAgent = GetComponent<NavMeshAgent>(); // Retrieves the NavMeshAgent attached to this GameObject

        if (playerCamera == null)
        {
            playerCamera = Camera.main; // If no camera is assigned, use the main camera
        }
    }

    private void Update()
    {
        // Retrieve horizontal input (A/D or left/right arrow keys) for movement
        float horizontalInput = Input.GetAxis("Horizontal"); // X-axis (left/right)
        float verticalInput = Input.GetAxis("Vertical");     // Z-axis (forward/backward)

        // Create a movement vector based on input
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // Apply movement based on the camera's orientation
        if (playerSettingsSO.canPlayerMove == true && moveDirection.magnitude > 0)
        {
            // Calculate the direction based on the camera's forward vector
            Vector3 cameraForward = playerCamera.transform.forward;
            cameraForward.y = 0f; // Ignore the Y-axis to prevent the character from moving up/down
            cameraForward.Normalize(); // Normalize to ensure consistent speed

            // Movement direction is based on the camera's orientation
            Vector3 cameraRight = playerCamera.transform.right;
            cameraRight.y = 0f;
            cameraRight.Normalize();

            Vector3 move = cameraForward * verticalInput + cameraRight * horizontalInput;

            // Use the NavMeshAgent's speed instead of a custom moveSpeed variable
            navMeshAgent.Move(move * navMeshAgent.speed * Time.deltaTime); // Use the NavMeshAgent's speed
        }

        // If the jump key (space) is pressed and the character is grounded
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Apply an impulse to jump
        }
    }

    // Checks if the character is grounded (to prevent double jumps)
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f); // A ray checks if the ground is below the character
    }
}